using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Polling.Contract
{
    public class VoteToEventDto
    {
        public Guid EventId { get; set; }
        public Guid CommunityId { get; set; }
        public Guid UserId { get; set; }
    }
}
