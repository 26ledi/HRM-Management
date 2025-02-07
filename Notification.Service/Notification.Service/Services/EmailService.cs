using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Mustache;
using Notification.Service.Configurations;
using Notification.Service.Models;

namespace Notification.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IOptionsSnapshot<WelcomeEmailSettings> _welcomeEmailoptions;
        private readonly IOptionsSnapshot<EmailSettings> _emailoptions;

        public EmailService(ILogger<EmailService> logger, IOptionsSnapshot<WelcomeEmailSettings> welcomeEmailoptions, IOptionsSnapshot<EmailSettings> emailoptions)
        {
            _logger = logger;
            _welcomeEmailoptions = welcomeEmailoptions;
            _emailoptions = emailoptions;
        }

        public async Task SendWelcomeEmailAsync(EmailReceiver receiver)
        {
            var templateEmailBody = File.ReadAllText(_welcomeEmailoptions.Value.BodyTemplatePath);
            var emailBody = Template.Compile(templateEmailBody).Render(receiver);

            await SendEmailAsync(receiver.Email, _welcomeEmailoptions.Value.Title, emailBody);

            _logger.LogInformation("The welcome email has been successfully sent to the email address {name}", receiver.Email);
        }
        private async Task SendEmailAsync(string emailAddress, string title, string emailBody)
        {
            try
            {

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_emailoptions.Value.EmailUsername));
                email.To.Add(MailboxAddress.Parse(emailAddress));
                email.Subject = title;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailBody };

                using var smtpClient = new MailKit.Net.Smtp.SmtpClient();
                await smtpClient.ConnectAsync(_emailoptions.Value.EmailHost, _emailoptions.Value.EmailPort, SecureSocketOptions.StartTlsWhenAvailable);
                await smtpClient.AuthenticateAsync(_emailoptions.Value.EmailUsername, _emailoptions.Value.EmailPassword);
                await smtpClient.SendAsync(email);
                await smtpClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"The sending email process failed, Error:{ex.Message}");
                throw;
            }
        }
    }
}
