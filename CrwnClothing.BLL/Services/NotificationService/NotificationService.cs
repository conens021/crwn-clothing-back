
using CrwnClothing.BLL.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CrwnClothing.BLL.Services.NotificationService
{
    public class NotificationService : INotificationService
    {

        private readonly MailSettings _settings;

        public NotificationService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendAsync(string subject,string recipientName, string recipientAddress, string template)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress(_settings.DisplayName,
            _settings.Mail);
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(recipientName, recipientAddress);
            message.To.Add(to);

            message.Subject = subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
           
            bodyBuilder.HtmlBody = template;


            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect(_settings.Host, _settings.Port, true);
            client.Authenticate(_settings.Mail, _settings.Password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            client.Dispose();
        }
      
    }
}
