using Application.Events.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Interfaces
{
	public interface IEventService
	{
		Task<Guid> CreateEvent(CreateEventRequestDto request);
		Task<List<EventsDto>> GetMyEvents(Guid id);
		Task<EventsDto> ShowEvent(Guid id);
        Task<List<EventsDto>> GetAllEvents();
        Task<bool> DeleteEventByOwnerAsync(Guid eventId);
	}
}
