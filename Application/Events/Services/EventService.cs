using Application.Events.Contract;
using Application.Events.Interfaces;
using AutoMapper;
using AutoMapper.Configuration;
using Domain.Event;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
                    EventId = Guid.NewGuid(),
                    UserId = request.UserId,
                    EventHeader = request.EventHeader,
                    EventDetail = request.EventDetail,
                    MinimumQuantity = request.MinimumQuantity,
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

        public async Task<bool> DeleteEventByOwnerAsync(Guid eventId)
        {
            try
            {
                Console.WriteLine("Event güncelleme işlemi başlatıldı.");

                var currentEvent = context.Events.FirstOrDefaultAsync(x => x.EventId == eventId).Result;
                if (currentEvent != null)
                {
                    Console.WriteLine("IsActive alanı güncelleniyor.");

                    currentEvent.IsActive = false;
                    context.Events.Update(currentEvent);
                    context.SaveChanges();

                    Console.WriteLine("Event güncellemesi başarılı.");

                    return true;
                }
                Console.WriteLine("Event bulunamadı.");
                return false;
            }
            catch (Exception ex)
            {
                // Hata mesajını loglamak için
                Console.WriteLine($"Hata: {ex.Message}");
                throw;
            }
        }


        public async Task<List<EventsDto>> GetAllEvents()
        {
            var events = context.Events.Where(x=>x.IsActive).ToList();

            var dtos = new List<EventsDto>();
            foreach (var item in events)
            {
                var dto = new EventsDto
                {
                    EventId = item.EventId,
                    UserId = Guid.Parse(item.UserId.ToString()),
                    EventHeader = item.EventHeader,
                    EventDetail = item.EventDetail,
                    MinimumQuantity = item.MinimumQuantity,
                    EventImage = item.EventImage,
                    Polling = item.Polling,
                    IsActive = item.IsActive,
                    CreatedDate = item.CreatedDate,
                    ModifiedDate = item.ModifiedDate
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<List<EventsDto>> GetMyEvents(Guid id)
        {
            var myEvents = context.Events.Where(x => x.UserId == id && x.IsActive).ToList();

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
                    MinimumQuantity = myEvent.MinimumQuantity,
                    Polling = myEvent.Polling,
                    IsActive = myEvent.IsActive,
                    CreatedDate = myEvent.CreatedDate,
                    ModifiedDate = myEvent.ModifiedDate
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<EventsDto> ShowEvent(Guid id)
        {
            var showEvent = context.Events.FirstOrDefault(x => x.EventId == id);
            var dto = new EventsDto();
            if (showEvent != null)
            {
                dto.EventId = showEvent.EventId;
                dto.UserId = Guid.Parse(showEvent.UserId.ToString());
                dto.EventHeader = showEvent.EventHeader;
                dto.EventDetail = showEvent.EventDetail;
                dto.EventImage = showEvent.EventImage;
                dto.MinimumQuantity = showEvent.MinimumQuantity;
                dto.Polling = showEvent.Polling;
                dto.IsActive = showEvent.IsActive;
                dto.CreatedDate = showEvent.CreatedDate;
                dto.ModifiedDate = showEvent.ModifiedDate;
            }

            return dto;
        }
    }
}
