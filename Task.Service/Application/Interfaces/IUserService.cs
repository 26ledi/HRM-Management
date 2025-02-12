using Contracts.Requests;
using Contracts.Responses;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> CreateAsync(UserRequest userRequest);
        Task<User> GetUserByIdAsync(Guid userId);
        Task<List<UserResponse>> GetAllAsync();
        Task<UserResponse> UpdateAsync(Guid userId);
        Task DeleteAsync(Guid taskId);
    }
}
