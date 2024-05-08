using Application.Polling.Contract;
using Application.Polling.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Polling.Services
{
    public class PollingService : IPollingService
    {
        private readonly AppDbContext context;
        public PollingService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> CreatePolling(PollingDto dto)
        {
            try
            {
                var newPolling = new Domain.Polling.Polling
                {
                    PollingId  = Guid.NewGuid(),
                    EventId =dto.EventId,
                    CommunityId = dto.CommunityId,
                    Quantity = 1,
                    IsActive = true,
                    CreatorUserId = dto.CreatorUserId,
                    CreatedDate = DateTime.Now
                };

                context.Pollings.Add(newPolling);
                context.SaveChanges();

                return newPolling.PollingId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<PollingDto>> GetPollingsByEvent(Guid eventId)
        {
            var pollings = context.Pollings.ToList();
            var dtos = new List<PollingDto>();

            foreach (var item in pollings)
            {
                var dto = new PollingDto
                {
                    PollingId = item.PollingId,
                    EventId = item.EventId,
                    CommunityId = item.CommunityId,
                    Quantity = item.Quantity,
                    IsActive = item.IsActive,
                    CreatorUserId = item.CreatorUserId,
                    CreatedDate = item.CreatedDate,
                    ModifierUserId = item.ModifierUserId,
                    ModifiedDate = item.ModifiedDate,
                };
                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
