using Application.Interfaces;
using Contracts.Requests;
using HRManagement.Message.Shared;
using MassTransit;

namespace Task.Service.API.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegisterMessage>
    {
        private readonly IUserService _userService;

        public UserRegisteredConsumer(IUserService userService)
        {
            _userService = userService;
        }
        public async System.Threading.Tasks.Task Consume(ConsumeContext<UserRegisterMessage> context)
        {

            var receiver = new UserRequest
            {
                Id = context.Message.Id,
                Email = context.Message.Email,
                Username = context.Message.Name
            };

            await _userService.CreateAsync(receiver);
        }
    }
}
