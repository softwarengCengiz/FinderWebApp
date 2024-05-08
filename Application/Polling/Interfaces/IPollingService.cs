using Application.Polling.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Polling.Interfaces
{
    public interface IPollingService
    {
        Task<List<PollingDto>> GetPollingsByEvent(Guid eventId);
        Task<Guid> CreatePolling(PollingDto dto);
    }
}
