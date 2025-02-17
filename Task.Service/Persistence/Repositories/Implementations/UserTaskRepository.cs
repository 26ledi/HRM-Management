using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Services.Abstractions.Repositories;

namespace Persistence.Repositories.Implementations
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly TaskDbContext _context;

        public UserTaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<UserTask> AddAsync(UserTask task)
        {
            _context.UserTasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task DeleteAsync(UserTask task)
        {
            _context.UserTasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserTask>> GetAllAsync()
        {
            return await _context.UserTasks.Include(x => x.User)
                                           .Include(x => x.TaskEvaluation)
                                           .ToListAsync();
        }

        public async Task<UserTask> GetByIdAsync(Guid id)
        {
            return await _context.UserTasks.AsNoTracking()
                                           .Include(x => x.User)
                                           .Include(x => x.TaskEvaluation)
                                           .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserTask> UpdateAsync(UserTask task)
        {
            _context.UserTasks.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<UserTask> GetByTitleAsync(string title) 
        {
            return await _context.UserTasks.Include(x => x.User)
                                           .Include(x => x.TaskEvaluation)
                                           .FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task <int> GetTotalTasksAssignedAsync() 
        {
            return await _context.UserTasks.CountAsync(x => x.UserEmail != ConstantMessage.NotAssigned);
        }

        public async Task<int> GetTasksCompletedOnTimeAsync()
        {
            return await _context.UserTasks
                .CountAsync(t => t.Status == UserTaskStatus.Completed && t.UpdatedAt != null && t.UpdatedAt <= t.Deadline);
        }

        public async Task<double> GetAverageTaskDelayAsync()
        {
            var delays = await _context.UserTasks
                .Where(t => t.Status == UserTaskStatus.Completed && t.UpdatedAt != null && t.UpdatedAt > t.Deadline)
                .Select(t => (t.UpdatedAt - t.Deadline).TotalMinutes) 
                .ToListAsync();

            return delays.Any() ? delays.Average() : 0; 

        }
    }
}
