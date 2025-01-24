using AutoMapper;
using HRManagement.BusinessLogic.DTOs;
using HRManagement.BusinessLogic.Helpers;
using HRManagement.BusinessLogic.Repositories.Interfaces;
using HRManagement.BusinessLogic.Services.Implementations;
using HRManagement.Exceptions.Shared;
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
        //  private readonly IPublishEndpoint _publishEndpoint;
        private readonly IConfiguration _configuration;

        public UserAuthenticationService(UserManager<IdentityUser> userManager, IMapper mapper, ILogger<UserService> logger, IConfiguration configuration, IOptionsSnapshot<JwtSettings> options)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _options = options;
        }

        public async Task<UserDto> CreateAsync(UserDto user, string password)
        {
            _logger.LogInformation("Process of adding a user...");
            var userLooked = await _userManager.FindByNameAsync(user.UserName);

            if (await _userManager.IsUserEmailExist(user.Email) && userLooked is not null)
            {
                _logger.LogError("This user with {email} or username {username} already exist", user.Email, user.UserName);

                throw new AlreadyExistsException("This user exists already");
            }

            user.Id = Guid.NewGuid().ToString();
            var identityUser = _mapper.Map<IdentityUser>(user);

            _logger.LogInformation("Starting the transaction for creating a new user with username {username} and email {email}", user.UserName, user.Email);
            IdentityResult result = await _userManager.CreateAsync(identityUser, password);
            _logger.LogInformation("The user has been successfully added...");
            await _userManager.AddRoleToUserAsync(user.Role, identityUser);
            _logger.LogInformation($"The role {user.Role} has been successfully assigned to {user.Name}...");
            //publish
 
            return _mapper.Map<UserDto>(identityUser);
        }

        public async Task<TokenDto> LoginAsync(UserLoginDto user)
        {
            _logger.LogInformation("Process of connecting a user...");
            var userLooked = await _userManager.FindByEmailAsync(user.Email)
                                               ?? throw new NotFoundException($"The user with this email : {user.Email} does not exist ");

            if (!await _userManager.CheckPasswordAsync(userLooked, user.Password))
            {
                _logger.LogError("There is no user with that email");
                throw new NotFoundException("Wrong email, please try again or sign up");
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
                     new Claim(ClaimTypes.Email , user.Email),
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
                Email = user.Email,
                Role = role,
                UserName = user.UserName,
            };
        }
    }
}
