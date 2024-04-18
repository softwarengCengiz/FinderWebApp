using Application.User.Contract;
using AutoMapper;
using FinderWebApp.Models.ApiRequest.Sign;
using FinderWebApp.Models.Request.Sign;

namespace FinderWebApp.Mappings.SignMappings
{
    public class SignProfile : Profile
    {
        public SignProfile()
        {
            CreateMap<SignUpRequest, UserDto>().ReverseMap();
            CreateMap<SignInRequest, UserDto>().ReverseMap();
        }
    }
}
