using AutoMapper;
using HRManagement.BusinessLogic.DTOs;
using HRManagement.BusinessLogic.Helpers;
using HRManagement.BusinessLogic.Repositories.Interfaces;
using HRManagement.Exceptions.Shared;
using HRManagement.Message.Shared;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HRManagement.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes an new instance cref of< see cref="UserService"/>
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public UserService(UserManager<IdentityUser> userManager, IMapper mapper, ILogger<UserService> logger, IPublishEndpoint publishEndpoint, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _configuration = configuration;
        }

        /// <summary>
        /// Function for changing a user's password.
        /// </summary>
        /// <param name="email">The user's email for whom the password will be changed.</param>
        /// <param name="currentPassword">The user's current password.</param>
        /// <param name="newPassword">The new password for the user.</param>
        /// <returns>Returns the UserDto object after the password change.</returns>
        /// <exception cref="NotFoundException">Thrown if the user is not found.</exception>
        public async Task<UserDto> ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            _logger.LogInformation("Attempting to change password for user with email {email}", email);

            var userLooked = await FindUserByEmailAsync(email);
            var result = await _userManager.ChangePasswordAsync(userLooked, currentPassword, newPassword);
            _logger.LogInformation("Password successfully changed for user with email {email}", email);

            return _mapper.Map<UserDto>(userLooked);
        }

        /// <summary>
        /// Function for validating the user's email.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="token">The email confirmation token.</param>
        /// <returns>Returns the UserDto object after confirming the email.</returns>
        /// <exception cref="NotFoundException">Thrown if the user is not found.</exception>>
        public async Task<UserDto> ConfirmEmailAsync(string email, string token)
        {
            _logger.LogInformation("Attempting to confirm email for user with email {email}", email);

            var user = await FindUserByEmailAsync(email);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            _logger.LogInformation("Email confirmed successfully for user with email {email}", email);

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Function for validating and resetting the password.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="token">The password reset token.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>Returns the UserDto object after resetting the password.</returns>
        /// <exception cref="NotFoundException">Thrown if the user is not found.</exception>
        public async Task<UserDto> ConfirmPasswordAsync(string email, string token, string newPassword)
        {
            _logger.LogInformation("Attempting to reset password for user with email {email}", email);

            var user = await FindUserByEmailAsync(email);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            _logger.LogInformation("Password reset successfully for user with email {email}", email);

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Function for deleting a user.
        /// </summary>
        /// <param name="email">The email of the user to be deleted.</param>
        /// <returns>Returns the deleted UserDto object.</returns>
        /// <exception cref="NotFoundException">Thrown if the user is not found.</exception>
        public async Task<UserDto> DeleteAsync(string email)
        {
            _logger.LogInformation("Attempting to delete user with email {email}", email);

            var user = await _userManager.FindByEmailAsync(email)
                                         ?? throw new NotFoundException($"The user with this email {email} does not exist");
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.First();
            var result = await _userManager.DeleteAsync(user);
            _logger.LogInformation("User with email {email} successfully deleted", email);

            //await _publishEndpoint.Publish(new UserDeletedMessage() { Id = new Guid(user.Id), Role = role });

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Function for sending a password reset token.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <returns>Returns the UserDto object.</returns>
        /// <exception cref="NotFoundException">Thrown if the user is not found.</exception>
        public async Task<UserDto> SendResetPasswordTokenAsync(string email)
        {
            _logger.LogInformation("Attempting to send reset password token for user with email {email}", email);

            var userEmailLooked = await FindUserByEmailAsync(email);
            var confirmationCode = await _userManager.GeneratePasswordResetTokenAsync(userEmailLooked);
            var userResetPasswordMessage = _mapper.Map<UserResetPasswordMessage>(userEmailLooked);
            userResetPasswordMessage.ConfirmationCode = confirmationCode;
            await _publishEndpoint.Publish(userResetPasswordMessage);

            _logger.LogInformation("Reset password token sent successfully for user with email {email}", email);

            return _mapper.Map<UserDto>(userEmailLooked);
        }

        /// <summary>
        /// Function for updating an existing user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<UserDto> UpdateAsync(UserDto user)
        {
            _logger.LogInformation("Attempting to update user with email {email}", user.Email);
            var userLooked = await _userManager.FindByEmailAsync(user.Email)
                                   ?? throw new NotFoundException($"The user with this email {user.Email} does not exist");
            var mappedUser = _mapper.Map(user, userLooked);
            var updatedUser = await _userManager.UpdateAsync(mappedUser);
            _logger.LogInformation($"The user with the email {user.Email} has been updated");

            return _mapper.Map<UserDto>(updatedUser);
        }

        /// <summary>
        /// Function for retrieving all users with pagination.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="size">The number of users per page.</param>
        /// <param name="cancellation">The cancellation token.</param>
        /// <returns>Returns a paginated list of UserDto objects.</returns>
        public async Task<PageResult<UserDto>> GetUsersAsync(int page, int size, CancellationToken cancellation)
        {
            _logger.LogInformation("Fetching users for page {page} with size {size}", page, size);

            var usersDto = new List<UserDto>();
            var users = await _userManager.Users
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellation);

            var count = users.Count();

            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user);
                var roles = await _userManager.GetRolesAsync(user);
                userDto.Role = roles.FirstOrDefault();
                usersDto.Add(userDto);
            }

            _logger.LogInformation("Fetched {count} users for page {page}", count, page);

            return new PageResult<UserDto>(size, page, count, usersDto);
        }

        /// <summary>
        /// Function for retrieving a user by email.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <returns>Returns the User object.</returns>
        /// <exception cref="NotFoundException">Thrown if the user is not found.</exception>
        public async Task<IdentityUser> FindUserByEmailAsync(string email)
        {
            _logger.LogInformation("Searching for user with email {email}", email);

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                _logger.LogError("This user with {email} does not exist", email);

                throw new NotFoundException("This user does not exist");
            }

            _logger.LogInformation("User with email {email} found", email);

            return user;
        }
    }
}
