using Notification.Service.Models;

namespace Notification.Service.Services
{
    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(EmailReceiver receiver);
    }
}
