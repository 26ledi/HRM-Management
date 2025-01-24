using HRManagement.BusinessLogic.DTOs;

namespace HRManagement.BusinessLogic.Repositories.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<UserDto> CreateAsync (UserDto user, string password);
        Task<TokenDto> LoginAsync (UserLoginDto user);
    }
}
