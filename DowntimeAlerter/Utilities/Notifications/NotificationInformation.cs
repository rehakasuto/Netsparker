using Microsoft.AspNetCore.Identity;

namespace DowntimeAlerter.Utilities.Notifications
{
    public class NotificationInformation
    {
        public NotificationInformation(IdentityUser reciever, string message)
        {
            Reciever = reciever;
            Message = message;
        }
        /// <summary>
        /// This property can be to mail address or phone number
        /// </summary>
        public IdentityUser Reciever { get; set; }
        /// <summary>
        /// The message of down time failure alert
        /// </summary>
        public string Message { get; set; }
    }
}
