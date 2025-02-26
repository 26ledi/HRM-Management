using Contracts.DTO_s;
using Contracts.Requests;
using Contracts.Responses;
using Domain.Entities;

namespace Services.Abstractions.Services
{
    public interface IUserTaskService
    {
        Task<UserTaskResponse> CreateTaskAsync(UserTaskRequest taskRequest);
        Task<UserTask> GetTaskByIdAsync(Guid taskId);
        Task<List<UserTaskResponse>> GetAllTasksAsync();
        Task<UserTaskResponse> UpdateTaskStatusAsync(Guid taskId, string status);
        Task<UserTaskResponse> UpdateTaskPriorityAsync(Guid taskId, string priority);
        Task<UpdateTaskResponse> UpdateTaskAsync(Guid taskId, UpdateTaskRequest updateTaskRequest);
        Task DeleteUserTaskAsync(Guid taskId);
        Task<UserTaskResponse> GetByTitleAsync(string title);
        Task<UserTaskResponse> UpdateTaskAssignmentAsync(Guid taskId, string userEmail);
        Task<UserTaskResponse> GetByIdAsync(Guid id);
        Task<TaskReportDto> GenerateTaskReportAsync();
    }
}
