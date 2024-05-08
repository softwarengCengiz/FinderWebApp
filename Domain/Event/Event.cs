using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Event
{
    public class Event
    {
        [Key]
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public string? EventHeader { get; set; }
        public string? EventDetail { get; set; }
        public int? MinimumQuantity { get; set; }
        public string? EventImage { get; set; }
        public Guid Polling { get; set; }
        public bool IsActive { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
	}
}
