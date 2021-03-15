using DowntimeAlerter.Utilities.Notifications;
using DowntimeAlerter.Web.Utilities.Notifications.Abstract;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace DowntimeAlerter.Web.Utilities.Notifications.Concrete
{
    public class MailService : INotificationService
    {
        private readonly MailConfig _mailConfig;
        private readonly string subject = $"Downtime Alert Failure {DateTime.Now.ToLongDateString()}";

        public MailService(IOptions<MailConfig> mailConfig)
        {
            _mailConfig = mailConfig.Value;
        }

        public Task SendMessageAsync(NotificationInformation notificationInformation)
        {
            var sendGridClient = new SendGridClient(_mailConfig.ApiKey);
            var from = new EmailAddress(_mailConfig.FromEmail, _mailConfig.FromUser);
            var to = new EmailAddress(notificationInformation.Reciever.Email);

            var mailMessage = MailHelper.CreateSingleEmail(from, to, subject, notificationInformation.Message, string.Empty);
            return sendGridClient.SendEmailAsync(mailMessage);
        }
    }
}
