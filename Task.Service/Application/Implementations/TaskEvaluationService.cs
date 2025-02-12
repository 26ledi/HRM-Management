using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
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
        public async Task<TaskEvaluationResponse> AddAsync(Guid taskId, TaskEvaluationRequest taskEvaluationRequest)
        {
            var task = await _userTaskRepository.GetByIdAsync(taskId)
                             ?? throw new NotFoundException($"A task with this ID: {taskId} does not exist");

            var mappedTaskEvaluation = new TaskEvaluation()
            {
                Id = Guid.NewGuid(),
                DateEvaluation = taskEvaluationRequest.DateEvaluation,
                EvaluatedBy = taskEvaluationRequest.EvaluatedBy,
                Rating = taskEvaluationRequest.Rating,
                UserTaskId = taskId,
            };

            await _taskEvaluationRepository.AddAsync(mappedTaskEvaluation);

            return new TaskEvaluationResponse()
            {
                Id = mappedTaskEvaluation.Id,
                DateEvaluation = mappedTaskEvaluation.DateEvaluation,
                EvaluatedBy = mappedTaskEvaluation.EvaluatedBy,
                Rating = mappedTaskEvaluation.Rating,
                UserTaskId = mappedTaskEvaluation.UserTaskId,
            };
        }

        public async Task DeleteAsync(Guid taskEvaluationId)
        {
            var taskEvaluation = await GetByIdAsync(taskEvaluationId);

            var mappedTaskEvaluation = new TaskEvaluation()
            {
                Id = taskEvaluationId,
                DateEvaluation = taskEvaluation.DateEvaluation,
                EvaluatedBy = taskEvaluation.EvaluatedBy,
                Rating = taskEvaluation.Rating,
                UserTaskId = taskEvaluation.UserTaskId,
            };

            await _taskEvaluationRepository.DeleteAsync(mappedTaskEvaluation);
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

        public async Task<TaskEvaluationResponse> GetByIdAsync(Guid id)
        {
             var taskEvaluation = await _taskEvaluationRepository.GetByIdAsync(id)
                                       ?? throw new NotFoundException($"A task with this ID: {id} does not exist");

            return new TaskEvaluationResponse()
            {
                Id = taskEvaluation.Id,
                DateEvaluation = taskEvaluation.DateEvaluation,
                EvaluatedBy = taskEvaluation.EvaluatedBy,
                Rating = taskEvaluation.Rating,
                UserTaskId = taskEvaluation.UserTaskId,
            };
        }

        public async Task<TaskEvaluationResponse> UpdateAsync(Guid taskId ,TaskEvaluationRequest taskEvaluationRequest)
        {
            var task = await _userTaskRepository.GetByIdAsync(taskId)
                              ?? throw new NotFoundException($"A task with this ID: {taskId} does not exist");

            var mappedTaskEvaluation = new TaskEvaluation()
            {
                Id = task.TaskEvaluation!.Id,
                DateEvaluation = taskEvaluationRequest.DateEvaluation,
                EvaluatedBy = taskEvaluationRequest.EvaluatedBy,
                Rating = taskEvaluationRequest.Rating,
                UserTaskId = taskId,
            };

            await _taskEvaluationRepository.UpdateAsync(mappedTaskEvaluation);

            return new TaskEvaluationResponse()
            {
                Id = mappedTaskEvaluation.Id,
                DateEvaluation = mappedTaskEvaluation.DateEvaluation,
                EvaluatedBy = mappedTaskEvaluation.EvaluatedBy,
                Rating = mappedTaskEvaluation.Rating,
                UserTaskId = mappedTaskEvaluation.UserTaskId,
            };
        }
    }
}
