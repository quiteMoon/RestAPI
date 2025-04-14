using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using WebApi.BLL.Configuration;

namespace WebApi.BLL.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;

            _smtpClient = new SmtpClient(_emailSettings.Host, _emailSettings.Port);
            _smtpClient.EnableSsl = true;
            _smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
        }

        public async Task SendMessageAsync(string to, string subject, string body, bool isHtml = false)
        {
            var message = new MailMessage(_emailSettings.Email ?? "", to, subject, body);
            message.IsBodyHtml = isHtml;
            await _smtpClient.SendMailAsync(message);
        }
    }
}
