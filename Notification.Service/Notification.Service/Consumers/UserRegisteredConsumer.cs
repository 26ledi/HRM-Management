using AutoMapper;
using HRManagement.Message.Shared;
using MassTransit;
using Notification.Service.Models;
using Notification.Service.Services;

namespace Notification.Service.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegisterMessage>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<UserRegisteredConsumer> _logger;
        private IMapper _mapper;

        public UserRegisteredConsumer(IEmailService emailService, ILogger<UserRegisteredConsumer> logger, IMapper mapper)
        {
            _emailService = emailService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UserRegisterMessage> context)
        {
            _logger.LogInformation("Message received from the broker: {@Message}", context.Message);

            if (context.Message == null)
            {
                _logger.LogError("Received message is null!");
                return;
            }

            var receiver = new EmailReceiver
            {
                Email = context.Message.Email,
                Name = context.Message.Name
            };

            if (receiver == null)
            {
                _logger.LogError("Mapping failed! Receiver is null.");
                return;
            }

            _logger.LogInformation("Receiver mapped successfully: {@Receiver}", receiver);

            await _emailService.SendWelcomeEmailAsync(receiver);
            _logger.LogInformation("Message Sent Successfully");
        }

    }
}
