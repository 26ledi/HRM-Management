using Application.Interfaces;
using Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Task.Service.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserControllers : ControllerBase
    {
        private readonly IUserService _userService;

        public UserControllers(IUserService userService)
        {
            _userService = userService;
        }

        //transform it to consumer
        [HttpPost("user")]
        public async Task<IActionResult> Create([FromBody] UserRequest userRequest)
        {
            var user = await _userService.CreateAsync(userRequest);

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userService.GetAllAsync();

            return Ok(user);
        }

        [HttpDelete("user{id}")]//Consumer
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            await _userService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByTaskId([FromRoute] Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            return Ok(user);
        }
    }
}
