using Application.Interfaces;
using HRManagement.Message.Shared;
using MassTransit;

namespace Task.Service.API.Consumers
{
    public class UserDeletedConsumer : IConsumer<UserDeletedMessage>
    {
        private readonly IUserService _userService;

        public UserDeletedConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async System.Threading.Tasks.Task Consume(ConsumeContext<UserDeletedMessage> context)
        {

            await _userService.DeleteAsync(context.Message.Email);
        }
    }
}
