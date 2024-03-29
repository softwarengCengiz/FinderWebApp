﻿using Application.Student.Contract;
using AutoMapper;

namespace Application.Student.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Domain.Student.Student, StudentDto>().ReverseMap();
        }
    }
}
