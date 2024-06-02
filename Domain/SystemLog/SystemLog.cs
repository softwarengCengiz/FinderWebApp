using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SystemLog
{
    public class SystemLog
    {
        [Key]
        public Guid LogId { get; set; }
        public string LogFrom { get; set; }
        public DateTime LogDate { get; set; }
        public Guid UserId { get; set; }
        public string LogDesc { get; set; }
    }
}
