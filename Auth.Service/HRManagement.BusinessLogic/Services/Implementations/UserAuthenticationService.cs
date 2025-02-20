using AutoMapper;
using HRManagement.BusinessLogic.DTOs;
using HRManagement.BusinessLogic.Helpers;
using HRManagement.BusinessLogic.Repositories.Interfaces;
using HRManagement.BusinessLogic.Services.Implementations;
using HRManagement.DataAccess.Constants;
using HRManagement.Exceptions.Shared;
using HRManagement.Message.Shared;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRManagement.BusinessLogic.Repositories.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IOptionsSnapshot<JwtSettings> _options;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IConfiguration _configuration;

        public UserAuthenticationService(UserManager<IdentityUser> userManager, IMapper mapper, ILogger<UserService> logger, IConfiguration configuration, IOptionsSnapshot<JwtSettings> options, IPublishEndpoint publishEndpoint)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _options = options;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<UserDto> CreateAsync(UserDto user, string password)
        {
            _logger.LogInformation("Process of adding a user...");
            var userLooked = await _userManager.FindByNameAsync(user.UserName);

            if (userLooked != null)
                throw new AlreadyExistsException($"This username '{user.UserName}' already exists");

            if (await _userManager.IsUserEmailExist(user.Email) || userLooked is not null)
            {
                _logger.LogError("This user with {email} or username {username} already exists", user.Email, user.UserName);
                throw new AlreadyExistsException($"This email '{user.Email}' already exists");
            }

            user.Id = Guid.NewGuid().ToString();
            var identityUser = _mapper.Map<IdentityUser>(user);

            _logger.LogInformation("Starting transaction for creating user {username}", user.UserName);

            IdentityResult result = await _userManager.CreateAsync(identityUser, password);
            if (!result.Succeeded)
            {
                _logger.LogError("Failed to create user {username}", user.UserName);
                throw new Exception("User creation failed");
            }

            _logger.LogInformation("User successfully created...");
            await _userManager.AddRoleToUserAsync(DefaultRole.Teacher, identityUser);
            _logger.LogInformation("Role {userRole} assigned to {username}", user.Role, user.UserName);

            var userRegisterMessage = _mapper.Map<UserRegisterMessage>(user);
            userRegisterMessage.Id = Guid.NewGuid(); 

            _logger.LogInformation("Publishing user registration message for {username}", user.UserName);
             await _publishEndpoint.Publish(userRegisterMessage, context =>
             {
                context.MessageId = userRegisterMessage.Id;
             });

            _logger.LogInformation("User registration message published successfully for {username}", user.UserName);
            return _mapper.Map<UserDto>(identityUser);
        }


        public async Task<TokenDto> LoginAsync(UserLoginDto user)
        {
            _logger.LogInformation("Process of connecting a user...");
            var userLooked = await _userManager.FindByEmailAsync(user.Email)
                                               ?? throw new NotFoundException($"The user with this email : {user.Email} does not exist ");

            if (!await _userManager.CheckPasswordAsync(userLooked, user.Password))
            {
                _logger.LogError("Re-check your email please!");
                throw new NotFoundException("Wrong password, please try again or sign up");
            }

            return await GenerateTokenAsync(userLooked);
        }

        private async Task<TokenDto> GenerateTokenAsync(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.Value.Key);
            var userRole = await _userManager.GetRolesAsync(user);
            var role = userRole.First();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.Email , user.Email!),
                     new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
                Audience = _options.Value.Audience,
                Issuer = _options.Value.Issuer,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenDto()
            {
                Id = user.Id,
                AccessToken = tokenHandler.WriteToken(token),
                DurationInMinutes = _options.Value.DurationInMinutes,
                Email = user.Email!,
                Role = role,
                UserName = user.UserName!,
            };
        }
    }
}
