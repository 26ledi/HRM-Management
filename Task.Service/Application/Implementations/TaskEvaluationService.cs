using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Domain.Constants;
using Domain.Entities;
using HRManagement.Exceptions.Shared;
using Services.Abstractions.Repositories;

namespace Application.Implementations
{
    public class TaskEvaluationService : ITaskEvaluationService
    {
        private readonly ITaskEvaluationRepository _taskEvaluationRepository;
        private readonly IUserTaskRepository _userTaskRepository;

        public TaskEvaluationService(ITaskEvaluationRepository taskEvaluationRepository, IUserTaskRepository userTaskRepository)
        {
            _taskEvaluationRepository = taskEvaluationRepository;
            _userTaskRepository = userTaskRepository;
        }
        public async Task<TaskEvaluationResponse> UpsertAsync(Guid taskId, TaskEvaluationRequest taskEvaluationRequest)
        {
            var task = await _userTaskRepository.GetByIdAsync(taskId)
                          ?? throw new NotFoundException($"A task with this ID: {taskId} was not found");

            var taskEvaluation = await _taskEvaluationRepository.GetByTaskIdAsync(taskId);

            if (taskEvaluation is null)
            {
                taskEvaluation = new TaskEvaluation
                {
                    Id = Guid.NewGuid(),
                    DateEvaluation = taskEvaluationRequest.DateEvaluation,
                    EvaluatedBy = ConstantMessage.HeadOfDepartment,
                    Rating = taskEvaluationRequest.Rating,
                    UserTaskId = taskId
                };

                await _taskEvaluationRepository.AddAsync(taskEvaluation);
            }
            else
            {
                taskEvaluation.DateEvaluation = taskEvaluationRequest.DateEvaluation;
                taskEvaluation.EvaluatedBy = ConstantMessage.HeadOfDepartment;
                taskEvaluation.Rating = taskEvaluationRequest.Rating;

                await _taskEvaluationRepository.UpdateAsync(taskEvaluation);
            }

            return new TaskEvaluationResponse
            {
                Id = taskEvaluation.Id,
                DateEvaluation = taskEvaluation.DateEvaluation,
                EvaluatedBy = taskEvaluation.EvaluatedBy,
                Rating = taskEvaluation.Rating,
                UserTaskId = taskEvaluation.UserTaskId
            };
        }


        public async Task<IEnumerable<TaskEvaluationResponse>> GetAllAsync()
        {
            var taskEvaluations = await _taskEvaluationRepository.GetAllAsync();

            if (taskEvaluations is null)
                throw new NotFoundException("There is no any taskEvaluation");

            return taskEvaluations.Select(taskEvaluation => new TaskEvaluationResponse()
            {
                Id = taskEvaluation.Id,
                DateEvaluation = taskEvaluation.DateEvaluation,
                EvaluatedBy = taskEvaluation.EvaluatedBy,
                Rating = taskEvaluation.Rating,
                UserTaskId = taskEvaluation.UserTaskId,
            }).ToList();
        }
    }
}
