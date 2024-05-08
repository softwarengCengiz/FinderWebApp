using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Community.Contract
{
    public class CreateCommunityDto
    {
        public string CommunityName { get; set; }
        public string CommunityDescription { get; set; }
        public string CommunityImage { get; set; }
        public Guid CreatorUserId { get; set; }
    }
}
