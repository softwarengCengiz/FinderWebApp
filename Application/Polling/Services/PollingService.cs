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

                //Aynı etkinlik için oylama başlatan kişi daha başlatamaz!
                var events = context.Pollings.Where(x => x.EventId == dto.EventId).ToList();
                if (!events.Select(x=>x.CreatorUserId).Contains(dto.CreatorUserId)) {
                    var newPolling = new Domain.Polling.Polling
                    {
                        PollingId = Guid.NewGuid(),
                        EventId = dto.EventId,
                        CommunityId = dto.CommunityId,
                        VotedUserIds = dto.CreatorUserId.ToString() + ";",
                        Quantity = 1,
                        IsActive = true,
                        CreatorUserId = dto.CreatorUserId,
                        CreatedDate = DateTime.Now
                    };

                    context.Pollings.Add(newPolling);
                    context.SaveChanges();

                    return newPolling.PollingId;
                }
                return Guid.NewGuid();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<PollingDto>> GetPollingsByEvent(Guid eventId)
        {
            var pollings = context.Pollings.Where(x=>x.EventId == eventId).ToList();
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

        public async Task<bool> VoteToEvent(VoteToEventDto dto)
        {
            var currentPolling = context.Pollings.FirstOrDefault(x=>x.EventId == dto.EventId && x.CommunityId == dto.CommunityId);

            if (currentPolling != null)
            {
                var votedUsers = currentPolling.VotedUserIds.Split(';'); 

                if (!votedUsers.Contains(dto.UserId.ToString())) //Oy kullanmış olan kişi aynı etkinlik için daha oy kullanamaz!
                {
                    currentPolling.Quantity += 1;
                    currentPolling.VotedUserIds = currentPolling.VotedUserIds + dto.UserId + ";";

                    context.Pollings.Update(currentPolling);
                    context.SaveChanges();

                    return true;
                }
            }
            return false;
        }
    }
}
