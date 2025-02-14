using Contracts.Requests;
using Contracts.Responses;
using Domain.Constants;
using Domain.Entities;
using HRManagement.Exceptions.Shared;
using Persistence.Repositories.Interfaces;
using Services.Abstractions.Repositories;
using Services.Abstractions.Services;

namespace Application.Implementations
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly IUserRepository _userRepository;

        public UserTaskService(IUserTaskRepository userTaskRepository, IUserRepository userRepository)
        {
            _userTaskRepository = userTaskRepository;
            _userRepository = userRepository;
        }

        public async Task<UserTaskResponse> CreateTaskAsync(UserTaskRequest taskRequest)
        {
            var taskLooked = await _userTaskRepository.GetByTitleAsync(taskRequest.Title);

            if (taskLooked is not null)
                throw new AlreadyExistsException($"A Task with this title : {taskRequest.Title} already exist");

            if (!TaskPriority.IsValidStatus(taskRequest.Priority))
                throw new Exception("This priority type does not exist");

            var mappedTask = new UserTask
            {
                Id = Guid.NewGuid(),
                Title = taskRequest.Title,
                Status = UserTaskStatus.Created,
                Priority = taskRequest.Priority,
                Description = taskRequest.Description,
                Deadline = taskRequest.Deadline,
                UserEmail = taskRequest.UserEmail,
                AttachmentUrl = taskRequest.AttachmentUrl,
                CreatedBy = taskRequest.CreatedBy,
            };

            await _userTaskRepository.AddAsync(mappedTask);
            //notify TimeTracking service  that a task  has been added

            return new UserTaskResponse()
            {
                Id = mappedTask.Id,
                CreatedBy = mappedTask.CreatedBy,
                Deadline = mappedTask.Deadline,
                Description = mappedTask.Description,
                Priority = mappedTask.Priority,
                Title = mappedTask.Title,
                TaskEvaluation = mappedTask.TaskEvaluation?.Rating.ToString() ?? "No rating yet",
                UserEmail = mappedTask.UserEmail,
                AttachmentUrl = mappedTask.AttachmentUrl,
                Status = mappedTask.Status,
            };
        }

        public async Task DeleteUserTaskAsync(Guid taskId)
        {
            var taskLooked = await GetTaskByIdAsync(taskId);

            await _userTaskRepository.DeleteAsync(taskLooked);
        }

        public async Task<List<UserTaskResponse>> GetAllTasksAsync()
        {
            var tasks = await _userTaskRepository.GetAllAsync();

            if (tasks is null)
                throw new NotFoundException("There is no any task");

            return tasks.Select(task => new UserTaskResponse()
            {
                Id = task.Id,
                CreatedBy = task.CreatedBy,
                Deadline = task.Deadline,
                Description = task.Description,
                Priority = task.Priority,
                Title = task.Title,
                Status = task.Status,
                TaskEvaluation = task.TaskEvaluation?.Rating.ToString() ?? "No rating yet",
                UserEmail = task.UserEmail,
                AttachmentUrl = task.AttachmentUrl
            }).ToList();
        }

        public async Task<UserTask> GetTaskByIdAsync(Guid taskId)
        {
            var userTaskLooked = await _userTaskRepository.GetByIdAsync(taskId)
                                       ?? throw new NotFoundException($"A task with this ID {taskId} does not exist");

            return userTaskLooked;
        }

        public async Task<UserTaskResponse> UpdateTaskStatusAsync(Guid taskId, string status)
        {
            var userTaskLooked = await GetTaskByIdAsync(taskId);

            if (UserTaskStatus.IsValidStatus(status))
                userTaskLooked.Status = status;

            await _userTaskRepository.UpdateAsync(userTaskLooked);

            return new UserTaskResponse
            {
                Id = taskId,
                Deadline = userTaskLooked.Deadline,
                Description = userTaskLooked.Description,
                Priority = userTaskLooked.Priority,
                Title = userTaskLooked.Title,
                Status = userTaskLooked.Status,
                TaskEvaluation = userTaskLooked.TaskEvaluation?.Rating.ToString() ?? "No rating yet",
                UserEmail = userTaskLooked.UserEmail,
                CreatedBy = userTaskLooked.CreatedBy,
                AttachmentUrl = userTaskLooked.AttachmentUrl
            };
        }
        public async Task<UserTaskResponse> UpdateTaskAssignmentAsync(Guid taskId, Guid userId)
        {
            var task = await GetTaskByIdAsync(taskId);
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                throw new NotFoundException($"A user with this ID:{userId} does not exist");

            task.UserId = userId;

            await _userTaskRepository.UpdateAsync(task);

            return new UserTaskResponse
            {
                Id = taskId,
                Deadline = task.Deadline,
                Description = task.Description,
                Priority = task.Priority,
                Title = task.Title,
                Status = task.Status,
                TaskEvaluation = task.TaskEvaluation?.Rating.ToString() ?? "No rating yet",
                UserEmail = task.UserEmail,
                CreatedBy = task.CreatedBy,
                AttachmentUrl = task.AttachmentUrl
            };
        }

        public async Task<UserTaskResponse> GetByTitleAsync(string title)
        {
            var userTaskLooked = await _userTaskRepository.GetByTitleAsync(title)
                                       ?? throw new NotFoundException($"A task with this title : {title} does not exist");
            return new UserTaskResponse
            {
                Id = userTaskLooked.Id,
                Deadline = userTaskLooked.Deadline,
                Description = userTaskLooked.Description,
                Priority = userTaskLooked.Priority,
                Title = userTaskLooked.Title,
                Status = userTaskLooked.Status,
                TaskEvaluation = userTaskLooked.TaskEvaluation?.Rating.ToString() ?? "No rating yet",
                UserEmail = userTaskLooked.UserEmail,
                CreatedBy = userTaskLooked.CreatedBy,
                AttachmentUrl = userTaskLooked.AttachmentUrl
            };
        }

        public async Task<UserTaskResponse> UpdateTaskPriorityAsync(Guid taskId, string priority)
        {
            var userTaskLooked = await GetTaskByIdAsync(taskId);

            if (TaskPriority.IsValidStatus(priority))
                userTaskLooked.Priority = priority;

            await _userTaskRepository.UpdateAsync(userTaskLooked);

            return new UserTaskResponse
            {
                Id = taskId,
                Deadline = userTaskLooked.Deadline,
                Description = userTaskLooked.Description,
                Priority = userTaskLooked.Priority,
                Title = userTaskLooked.Title,
                Status = userTaskLooked.Status,
                TaskEvaluation = userTaskLooked.TaskEvaluation?.Rating.ToString() ?? "No rating yet",
                UserEmail = userTaskLooked.UserEmail,
                CreatedBy = userTaskLooked.CreatedBy,
                AttachmentUrl = userTaskLooked.AttachmentUrl
            };
        }

        public async Task<UpdateTaskResponse> UpdateTaskAsync(Guid taskId, UpdateTaskRequest updateTaskRequest)
        {
            var task = await GetTaskByIdAsync(taskId);

            task.Title = updateTaskRequest.Title;
            task.Description = updateTaskRequest.Description;
            task.AttachmentUrl = updateTaskRequest.AttachmentUrl;
            task.Deadline = updateTaskRequest.Deadline;

            var updatedTask = await _userTaskRepository.UpdateAsync(task);

            return new UpdateTaskResponse
            {
                Id = taskId,
                Deadline = updatedTask.Deadline,
                Description = updatedTask.Description,
                Title = updatedTask.Title,
                AttachmentUrl = updatedTask.AttachmentUrl
            };
        }
    }
}
