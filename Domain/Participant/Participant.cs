using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Participant
{
    public class Participant
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Job { get; set; }
        public string? Company { get; set; }
        public string? CV { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
