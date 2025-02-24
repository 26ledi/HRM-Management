using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Task.Service.API.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class UserControllers : ControllerBase
    {
        private readonly IUserService _userService;

        public UserControllers(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userService.GetAllAsync();

            return Ok(user);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByTaskId([FromRoute] Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            return Ok(user);
        }
    }
}
