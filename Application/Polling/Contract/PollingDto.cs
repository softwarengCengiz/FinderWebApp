using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Polling.Contract
{
    public class PollingDto
    {
        public Guid PollingId { get; set; }
        public Guid EventId { get; set; }
        public Guid CommunityId { get; set; }
        public string VotedUserIds { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatorUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifierUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
