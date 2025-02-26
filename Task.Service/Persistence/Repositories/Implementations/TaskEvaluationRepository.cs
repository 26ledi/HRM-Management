using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Services.Abstractions.Repositories;

namespace Persistence.Repositories.Implementations
{
    public class TaskEvaluationRepository : ITaskEvaluationRepository
    {
        private readonly TaskDbContext _context;

        public TaskEvaluationRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<TaskEvaluation> AddAsync(TaskEvaluation task)
        {
            _context.TaskEvaluations.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task DeleteAsync(TaskEvaluation task)
        {
            _context.TaskEvaluations.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskEvaluation>> GetAllAsync()
        {
            return await _context.TaskEvaluations
                                 .Include(x => x.UserTask)
                                 .ToListAsync();
        }

        public async Task<TaskEvaluation> GetByTaskIdAsync(Guid id)
        {
            return await _context.TaskEvaluations.AsNoTracking()
                                                 .FirstOrDefaultAsync(x => x.UserTaskId == id);
        }

        public async Task<TaskEvaluation> UpdateAsync(TaskEvaluation task)
        {
            _context.TaskEvaluations.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }
    }
}
