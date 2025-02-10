using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Services.Abstractions.Repositories;

namespace Persistence.Repositories
{
    public class TaskAssignmentRepository : ITaskAssignmentRepository
    {
        private readonly TaskDbContext _context;

        public TaskAssignmentRepository(TaskDbContext context)
        {
            _context = context;
        }
  
        public async Task<TaskAssignment> AddAsync(TaskAssignment task)
        {
            _context.TaskAssignments.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task DeleteAsync(TaskAssignment task)
        {
            _context.TaskAssignments.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskAssignment> UpdateAsync(TaskAssignment task)
        {
            _context.TaskAssignments.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<IEnumerable<TaskAssignment>> GetAllAsync()
        {
            return await _context.TaskAssignments.ToListAsync();
        }

        public async Task<TaskAssignment> GetByIdAsync(Guid id)
        {
            return await _context.TaskAssignments.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
