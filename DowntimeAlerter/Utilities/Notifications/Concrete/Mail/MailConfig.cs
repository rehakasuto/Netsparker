namespace DowntimeAlerter.Web.Utilities.Notifications.Concrete
{
    public class MailConfig
    {
        /// <summary>
        /// For sending mail i prefered to use Send Grid. 
        /// Api key of Send grid.
        /// </summary>
        public string ApiKey { get; set; }
        public string FromEmail { get; set; }
        public string FromUser { get; set; }
    }
}
