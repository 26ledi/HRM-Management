using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Services.Abstractions.Repositories;

namespace Persistence.Repositories
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
            return await _context.UserTasks.Include(x => x.TaskAssignments)
                                           .Include(x => x.TaskEvaluation)
                                           .ToListAsync();
        }

        public async Task<UserTask> GetByIdAsync(Guid id)
        {
            return await _context.UserTasks.Include(x => x.TaskAssignments)
                                           .Include(x => x.TaskEvaluation)
                                           .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserTask> UpdateAsync(UserTask task)
        {
            _context.UserTasks.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }
    }
}
