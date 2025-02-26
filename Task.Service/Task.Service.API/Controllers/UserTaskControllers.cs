using Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Services;

namespace Task.Service.API.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class UserTaskControllers : ControllerBase
    {
        private readonly IUserTaskService _taskService;

        public UserTaskControllers(IUserTaskService taskService)
        {
            _taskService = taskService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("task")]
        public async Task<IActionResult> CreateTask([FromBody] UserTaskRequest taskRequest)
        {
            var task = await _taskService.CreateTaskAsync(taskRequest);

            return Ok(task);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var task = await _taskService.GetAllTasksAsync();

            return Ok(task);
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateTaskStatus([FromRoute] Guid id, string status)
        {
            var task = await _taskService.UpdateTaskStatusAsync(id, status);

            return Ok(task);
        }

       // [Authorize(Roles = "Admin")]
        [HttpPut("assignment/{id}")]
        public async Task<IActionResult> UpdateTaskAssignmentAsync([FromRoute] Guid id, string userEmail)
        {
            var task = await _taskService.UpdateTaskAssignmentAsync(id, userEmail);

            return Ok(task);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("priority/{id}")]
        public async Task<IActionResult> UpdateTaskPriority([FromRoute] Guid id, string priority)
        {
            var task = await _taskService.UpdateTaskPriorityAsync(id, priority);

            return Ok(task);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("task/{taskId}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid taskId, [FromBody] UpdateTaskRequest updateTaskRequest)
        {
            var task = await _taskService.UpdateTaskAsync(taskId, updateTaskRequest);

            return Ok(task);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("task/{taskId}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid taskId)
        {
            await _taskService.DeleteUserTaskAsync(taskId);

            return NoContent();
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> GetByTitle([FromRoute] string title)
        {
            var task = await _taskService.GetByTitleAsync(title);

            return Ok(task);
        }

        [HttpGet("task/{id}")]
        public async Task<IActionResult> GetByTaskId([FromRoute] Guid id)
        {
            var task = await _taskService.GetByIdAsync(id);

            return Ok(task);
        }

        [HttpGet("task-reports")]
        public async Task<IActionResult> GetTaskReports()
        {
            var reports = await _taskService.GenerateTaskReportAsync();

            return Ok(reports);
        }
    }
}
