using AutoMapper;
using HRManagement.BusinessLogic.DTOs;
using HRManagement.BusinessLogic.Helpers;
using HRManagement.BusinessLogic.Repositories.Interfaces;
using HRManagement.Exceptions.Shared;
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
        // private readonly IPublishEndpoint _publishEndpoint;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper, ILogger<UserService> logger, /*IPublishEndpoint publishEndpoint*/ IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            // _publishEndpoint = publishEndpoint;
            _configuration = configuration;
        }

        public async Task<UserDto> ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            _logger.LogInformation("Attempting to change password for user with email {email}", email);

            var userLooked = await FindUserByEmailAsync(email);
            var result = await _userManager.ChangePasswordAsync(userLooked, currentPassword, newPassword);
            _logger.LogInformation("Password successfully changed for user with email {email}", email);

            return _mapper.Map<UserDto>(userLooked);
        }

        public async Task<UserDto> DeleteAsync(string email)
        {
            _logger.LogInformation("Attempting to delete user with email {email}", email);

            var user = await FindUserByEmailAsync(email)
                                         ?? throw new NotFoundException($"The user with this email {email} does not exist");
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.First();
            var result = await _userManager.DeleteAsync(user);
            _logger.LogInformation("User with email {email} successfully deleted", email);

            //await _publishEndpoint.Publish(new UserDeletedMessage() { Id = new Guid(user.Id), Role = role });

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(string email, UserDto user)
        {
            _logger.LogInformation("Attempting to update user with email {email}", email);

            var userLooked = await FindUserByEmailAsync(email);

            if (!string.IsNullOrWhiteSpace(user.UserName))
                userLooked.UserName = user.UserName;

            if (!string.IsNullOrWhiteSpace(user.Email))
                userLooked.Email = user.Email;
            var result = await _userManager.UpdateAsync(userLooked);

            if (!result.Succeeded)
            {
                _logger.LogError("Failed to update user: {errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                throw new Exception("Failed to update user.");
            }

            if (!string.IsNullOrWhiteSpace(user.Role))
            {
                var currentRoles = await _userManager.GetRolesAsync(userLooked);
                await _userManager.RemoveFromRolesAsync(userLooked, currentRoles);
                await _userManager.AddToRoleAsync(userLooked, user.Role);
            }

            _logger.LogInformation("User with email {email} successfully updated", email);

            return _mapper.Map<UserDto>(user);
        }


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
                userDto.Role = roles.First();
                usersDto.Add(userDto);
            }

            _logger.LogInformation("Fetched {count} users for page {page}", count, page);

            return new PageResult<UserDto>(size, page, count, usersDto);
        }

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
