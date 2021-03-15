using DowntimeAlerter.Helpers;
using System.ComponentModel.DataAnnotations;

namespace DowntimeAlerter.Models
{
    public class TargetApplication : Base
    {
        [MaxLength(100), Required]
        public string Name { get; set; }
        [MaxLength(500), Required]
        public string Url { get; set; }
        /// <summary>
        /// The interval property must be cron expression to trigger hangfire properly
        /// </summary>
        [MaxLength(50), Required]
        public string Interval { get; set; }
        public string UserId { get; set; }
    }
}
