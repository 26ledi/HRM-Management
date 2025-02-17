using Domain.Entities;

namespace Services.Abstractions.Repositories
{
    public interface IUserTaskRepository
    {
        Task<UserTask> GetByIdAsync(Guid id);
        Task<UserTask> GetByTitleAsync(string title);
        Task<IEnumerable<UserTask>> GetAllAsync();
        Task<UserTask>AddAsync(UserTask task);
        Task<UserTask>UpdateAsync(UserTask task);
        Task DeleteAsync(UserTask task);
        Task<int> GetTotalTasksAssignedAsync();
        Task<int> GetTasksCompletedOnTimeAsync();
        Task<double> GetAverageTaskDelayAsync();
    }
}
