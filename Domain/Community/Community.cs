using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Community
{
    public class Community
    {
        [Key]
        public Guid CommunityId { get; set; }
        public string? CommunityName { get; set; }
        public string? CommunityDescription { get; set; }
        public string? OldEventsIds { get; set; }
        public Guid CreatorUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifierUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
