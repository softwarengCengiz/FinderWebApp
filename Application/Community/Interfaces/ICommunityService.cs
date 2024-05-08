using Application.Community.Contract;
using Application.Events.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Community.Interfaces
{
    public interface ICommunityService
    {
        Task<Guid> CreateCommunity(CreateCommunityDto request);
        Task<List<CommunityDto>> GetAllCommunities();
    }
}
