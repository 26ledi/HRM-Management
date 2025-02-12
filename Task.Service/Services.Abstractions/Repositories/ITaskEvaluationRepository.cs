using Domain.Entities;

namespace Services.Abstractions.Repositories
{
    public interface ITaskEvaluationRepository
    {
        Task<TaskEvaluation> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskEvaluation>> GetAllAsync();
        Task<TaskEvaluation> AddAsync(TaskEvaluation task);
        Task<TaskEvaluation> UpdateAsync(TaskEvaluation task);
        Task DeleteAsync(TaskEvaluation task);
    }
}
