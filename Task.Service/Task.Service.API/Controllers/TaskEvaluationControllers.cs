using Application.Interfaces;
using Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Task.Service.API.Controllers
{
    [ApiController]
    [Route("taskEvaluations")]
    public class TaskEvaluationControllers : ControllerBase
    {
        private readonly ITaskEvaluationService _taskEvaluationService;

        public TaskEvaluationControllers(ITaskEvaluationService taskEvaluationService)
        {
            _taskEvaluationService = taskEvaluationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("taskEvaluation/{taskId}")]
        public async Task<IActionResult> Create([FromRoute] Guid taskId, [FromBody] TaskEvaluationRequest taskEvaluationRequest)
        {
            var taskEvaluation = await _taskEvaluationService.AddAsync(taskId, taskEvaluationRequest);

            return Ok(taskEvaluation);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("taskEvaluation/{taskId}")]
        public async Task<IActionResult> Update([FromRoute] Guid taskId, [FromBody] TaskEvaluationRequest taskEvaluationRequest)
        {
            var taskEvaluation = await _taskEvaluationService.UpdateAsync(taskId, taskEvaluationRequest);

            return Ok(taskEvaluation);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var taskEvaluations = await _taskEvaluationService.GetAllAsync();

            return Ok(taskEvaluations);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("taskEvaluation{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            await _taskEvaluationService.DeleteAsync(id);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("taskEvaluation/{id}")]
        public async Task<IActionResult> GetByTaskId([FromRoute] Guid id)
        {
            var user = await _taskEvaluationService.GetByIdAsync(id);

            return Ok(user);
        }
    }
}
