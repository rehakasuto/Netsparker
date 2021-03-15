using DowntimeAlerter.Web.Utilities.Notifications.Abstract;
using System;
using System.Threading.Tasks;

namespace DowntimeAlerter.Utilities.Notifications.Concrete.Sms
{
    public class SmsService : INotificationService
    {
        public Task SendMessageAsync(NotificationInformation notificationInformation)
        {
            throw new NotImplementedException();
        }
    }
}
