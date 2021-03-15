using System;
using System.ComponentModel.DataAnnotations;

namespace DowntimeAlerter.Models
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
