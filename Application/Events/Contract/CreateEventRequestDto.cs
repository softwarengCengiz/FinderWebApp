using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Contract
{
	public class CreateEventRequestDto
	{
		public Guid UserId { get; set; }
		public string EventHeader { get; set; }
		public string EventDetail { get; set; }
        public int? MinimumQuantity { get; set; }
        public string EventImage { get; set; }
		public bool IsActive { get; set; }
	}
}
