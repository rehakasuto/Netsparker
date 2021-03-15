using DowntimeAlerter.Web.Utilities.Notifications.Abstract;
using System.Collections.Generic;

namespace DowntimeAlerter.Utilities.Notifications
{
    public class NotificationManager
    {
        private readonly IEnumerable<INotificationService> _notificationServices;

        public NotificationManager(IEnumerable<INotificationService> notificationServices)
        {
            _notificationServices = notificationServices;
        }

        //this function exists to support sending multiple notifications
        public void SendAll(NotificationInformation notificationInformation)
        {
            foreach (var notificationService in _notificationServices)
            {
                notificationService.SendMessageAsync(notificationInformation);
            }
        }
    }
}
