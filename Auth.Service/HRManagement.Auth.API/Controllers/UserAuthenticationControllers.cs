using AutoMapper;
using HRManagement.Auth.API.Requests;
using HRManagement.Auth.API.Responses;
using HRManagement.BusinessLogic.DTOs;
using HRManagement.BusinessLogic.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Auth.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class UserAuthenticationControllers : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IMapper _mapper;
        public UserAuthenticationControllers(IMapper mapper, IUserAuthenticationService userAuthenticationService)
        {
            _mapper = mapper;
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLoginRequest)
        {
            var token = await _userAuthenticationService.LoginAsync(_mapper.Map<UserLoginDto>(userLoginRequest));

            return Ok(_mapper.Map<UserLoginResponse>(token));
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            var response = await _userAuthenticationService.CreateAsync(_mapper.Map<UserDto>(userRegisterRequest), userRegisterRequest.Password);

            return Ok(_mapper.Map<UserRegisterResponse>(response));
        }
    }
}

