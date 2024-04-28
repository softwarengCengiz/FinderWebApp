using Application.Events.Contract;
using Application.Events.Interfaces;
using AutoMapper;
using AutoMapper.Configuration;
using Domain.Event;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public EventService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Guid> CreateEvent(CreateEventRequestDto request)
        {
            try
            {
                var newEvent = new Domain.Event.Event
                {
                    EventId = new Guid(),
                    UserId = request.UserId,
                    EventHeader = request.EventHeader,
                    EventDetail = request.EventDetail,
                    EventImage = request.EventImage,
                    IsActive = request.IsActive,
                    CreatedDate = DateTime.Now
                };

                await context.Events.AddAsync(newEvent);

                await context.SaveChangesAsync();

                return newEvent.EventId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EventsDto>> GetMyEvents(Guid id)
        {
            var myEvents = context.Events.Where(x => x.UserId == id).ToList();

            var dtos = new List<EventsDto>();
            foreach (var myEvent in myEvents)
            {
                var dto = new EventsDto
                {
                    EventId = myEvent.EventId,
                    UserId = Guid.Parse(myEvent.UserId.ToString()),
                    EventHeader = myEvent.EventHeader,
                    EventDetail = myEvent.EventDetail,
                    EventImage = myEvent.EventImage,
                    Polling = myEvent.Polling,
                    IsActive = myEvent.IsActive,
                    CreatedDate = myEvent.CreatedDate,
                    ModifiedDate = myEvent.ModifiedDate
                };

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
