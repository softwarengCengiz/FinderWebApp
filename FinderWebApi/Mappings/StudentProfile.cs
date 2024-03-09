using Application.Student.Contract;
using AutoMapper;
using FinderWebApi.Models;

namespace FinderWebApi.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDto, Student>().ReverseMap();
        }
    }
}
