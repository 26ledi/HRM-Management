using Contracts.Requests;
using Contracts.Responses;

namespace Application.Interfaces
{
    public interface ITaskEvaluationService
    {
        Task<TaskEvaluationResponse> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskEvaluationResponse>> GetAllAsync();
        Task<TaskEvaluationResponse> AddAsync(Guid taskId, TaskEvaluationRequest taskEvaluationRequest);
        Task<TaskEvaluationResponse> UpdateAsync(Guid taskId, TaskEvaluationRequest taskEvaluationRequest);
        Task DeleteAsync(Guid taskEvaluationId);
    }
}
