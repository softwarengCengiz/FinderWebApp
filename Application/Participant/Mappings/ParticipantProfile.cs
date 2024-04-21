using Application.Participant.Contract;
using Application.Student.Contract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Participant.Mappings
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            CreateMap<Domain.Participant.Participant, ParticipantDto>().ReverseMap();
        }
    }
}
