using AutoMapper;
using HRManagement.Auth.API.Requests;
using HRManagement.Auth.API.Responses;
using HRManagement.BusinessLogic.DTOs;
using HRManagement.BusinessLogic.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Auth.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut("user/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromRoute] string email, [FromBody] UserUpdateRequest userUpdateRequest)
        {
            var user = await _userService.UpdateAsync(email, _mapper.Map<UserDto>(userUpdateRequest));
            var response = _mapper.Map<UserResponse>(user);

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("user/{email}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteAsync(string email)
        {
            var result = await _userService.DeleteAsync(email);
            var response = _mapper.Map<UserResponse>(result);

            return NoContent();
        }

        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] UserChangePasswordRequest userChangePasswordRequest)
        {
            var user = await _userService.ChangePasswordAsync(userChangePasswordRequest.Email, userChangePasswordRequest.CurrentPassword, userChangePasswordRequest.NewPassword);
            var response = _mapper.Map<UserChangePasswordResponse>(user);

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsersByPageAsync([FromQuery] int page, [FromQuery] int size, CancellationToken cancellation)
        {
            return Ok(await _userService.GetUsersAsync(page, size, cancellation));
        }
    }
}
