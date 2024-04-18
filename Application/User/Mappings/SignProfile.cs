using Application.User.Contract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Mappings
{
    public class SignProfile : Profile
    {
        public SignProfile()
        {
            CreateMap<Domain.User.User,UserDto>().ReverseMap();
        }
    }
}
