using DowntimeAlerter.Utilities.Notifications;
using System.Threading.Tasks;

namespace DowntimeAlerter.Web.Utilities.Notifications.Abstract
{
    public interface INotificationService
    {
        Task SendMessageAsync(NotificationInformation notificationInformation);
    }
}
