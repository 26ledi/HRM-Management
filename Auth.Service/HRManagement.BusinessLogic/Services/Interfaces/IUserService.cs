using HRManagement.BusinessLogic.DTOs;
using HRManagement.BusinessLogic.Helpers;

namespace HRManagement.BusinessLogic.Repositories.Interfaces
{    /// <summary>
     /// Interface defining operations for managing users.
     /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Function for changing a user's password.
        /// </summary>
        /// <param name="email">The user's email for whom the password will be changed.</param>
        /// <param name="currentPassword">The user's current password.</param>
        /// <param name="newPassword">The new password for the user.</param>
        /// <returns>Returns the UserDto object after the password change.</returns>
        Task<UserDto> ChangePasswordAsync(string email, string currentPassword, string newPassword);

        /// <summary>
        /// Function for updating an existing user.
        /// </summary>
        /// <param name="user">The updated user details.</param>
        /// <returns>Returns the updated UserDto object.</returns>
        Task<UserDto> UpdateAsync(UserDto user);

        /// <summary>
        /// Function for deleting a user.
        /// </summary>
        /// <param name="email">The email of the user to be deleted.</param>
        /// <returns>Returns the deleted UserDto object.</returns>
        Task<UserDto> DeleteAsync(string email);

        /// <summary>
        /// Function for sending a password reset token.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <returns>Returns the UserDto object.</returns>
        Task<UserDto> SendResetPasswordTokenAsync(string email);

        /// <summary>
        /// Function for validating the user's email.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="token">The email confirmation token.</param>
        /// <returns>Returns the UserDto object after confirming the email.</returns>
        Task<UserDto> ConfirmPasswordAsync(string email, string token, string newPassword);

        /// <summary>
        /// Function for validating Email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UserDto> ConfirmEmailAsync(string email, string token);

        /// <summary>
        /// Function for retrieving all users with pagination.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="size">The number of users per page.</param>
        /// <param name="cancellation">The cancellation token.</param>
        /// <returns>Returns a paginated list of UserDto objects.</returns>
        Task<PageResult<UserDto>> GetUsersAsync(int page, int size, CancellationToken cancellation);
    }
}
