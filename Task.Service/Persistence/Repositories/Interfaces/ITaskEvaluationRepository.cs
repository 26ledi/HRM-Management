using Domain.Entities;

namespace Services.Abstractions.Repositories
{
    public interface ITaskEvaluationRepository
    {
        Task<TaskEvaluation> GetByTaskIdAsync(Guid taskId);
        Task<IEnumerable<TaskEvaluation>> GetAllAsync();
        Task<TaskEvaluation> AddAsync(TaskEvaluation taskEvaluation);
        Task<TaskEvaluation> UpdateAsync(TaskEvaluation taskEvaluation);
        Task DeleteAsync(TaskEvaluation task);
    }
}
