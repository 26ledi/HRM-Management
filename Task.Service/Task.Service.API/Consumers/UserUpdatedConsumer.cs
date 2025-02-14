using Application.Interfaces;
using Contracts.Requests;
using HRManagement.Messages.Shared;
using MassTransit;

namespace Task.Service.API.Consumers
{
    public class UserUpdatedConsumer : IConsumer<UserUpdatedMessage>
    {
        private readonly IUserService _userService;

        public UserUpdatedConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async  System.Threading.Tasks.Task Consume(ConsumeContext<UserUpdatedMessage> context)
        {
            var receiver = new UserRequest()
            {
                Email = context.Message.Email,
                Username = context.Message.Username,
            };

            await _userService.UpdateAsync(context.Message.UserEmailToUpdate, receiver);
        }
    }
}
