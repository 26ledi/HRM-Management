using HRManagement.BusinessLogic.DTOs;

namespace HRManagement.BusinessLogic.Repositories.Interfaces
{
    public interface IUserAuthenticationService
    {
        /// <summary>
        /// Function for creating a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<UserDto> CreateAsync (UserDto user, string password);

        /// <summary>
        /// Function for login a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<TokenDto> LoginAsync (UserLoginDto user);
    }
}
