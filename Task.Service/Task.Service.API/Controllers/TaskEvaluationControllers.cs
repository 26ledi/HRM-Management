using Application.Interfaces;
using Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Task.Service.API.Controllers
{
    [ApiController]
    [Route("task-evaluations")]
    public class TaskEvaluationControllers : ControllerBase
    {
        private readonly ITaskEvaluationService _taskEvaluationService;

        public TaskEvaluationControllers(ITaskEvaluationService taskEvaluationService)
        {
            _taskEvaluationService = taskEvaluationService;
        }

        //       [Authorize(Roles = "Admin")]
        [HttpPost("task-evaluation/{taskId}/upsert")]
        public async Task<IActionResult> Upsert([FromRoute] Guid taskId, [FromBody] TaskEvaluationRequest taskEvaluationRequest)
        {
            var taskEvaluation = await _taskEvaluationService.UpsertAsync(taskId, taskEvaluationRequest);

            return Ok(taskEvaluation);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var taskEvaluations = await _taskEvaluationService.GetAllAsync();

            return Ok(taskEvaluations);
        }
    }
}
