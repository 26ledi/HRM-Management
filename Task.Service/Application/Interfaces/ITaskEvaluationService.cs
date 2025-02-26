using Contracts.Requests;
using Contracts.Responses;

namespace Application.Interfaces
{
    public interface ITaskEvaluationService
    {
        Task<IEnumerable<TaskEvaluationResponse>> GetAllAsync();
        Task<TaskEvaluationResponse> UpsertAsync(Guid taskId, TaskEvaluationRequest request);
    }
}
