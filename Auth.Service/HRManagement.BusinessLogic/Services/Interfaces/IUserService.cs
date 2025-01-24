using HRManagement.BusinessLogic.DTOs;
using HRManagement.BusinessLogic.Helpers;

namespace HRManagement.BusinessLogic.Repositories.Interfaces
{    /// <summary>
     /// Interface defining operations for managing users.
     /// </summary>
    public interface IUserService
    {
        Task<UserDto> ChangePasswordAsync(string email, string currentPassword, string newPassword);
        Task<UserDto> UpdateAsync(string email, UserDto user);
        Task<UserDto> DeleteAsync(string email);
        Task<PageResult<UserDto>> GetUsersAsync(int page, int size, CancellationToken cancellation);
    }
}
