using Domain.Entities;

namespace Services.Abstractions.Repositories
{
    public interface ITaskAssignmentRepository
    {
        Task<TaskAssignment> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskAssignment>> GetAllAsync();
        Task<TaskAssignment> AddAsync(TaskAssignment task);
        Task<TaskAssignment> UpdateAsync(TaskAssignment task);
        Task DeleteAsync(TaskAssignment task);
    }
}
