using Application.Events.Contract;
using AutoMapper;
using Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Mappings
{
	public class EventsProfile : Profile
	{
		public EventsProfile()
		{
			CreateMap<Event, EventsDto>().ReverseMap();
		}
	}
}
