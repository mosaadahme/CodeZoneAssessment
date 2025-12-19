using AutoMapper;
using CodeZone.Core.DTOs.Courses;
using CodeZone.Core.DTOs.Students;
using CodeZone.Core.Entities;

namespace CodeZone.Services.Students
{
    public class StudentMapping : Profile
    {
        public StudentMapping ( )
        {
            CreateMap<Student, StudentResponse> ( )
                  .ForMember ( dest => dest.BirthDateFormatted,
                  opt => opt.MapFrom ( src => src.BirthDate.ToShortDateString ( ) ) );

            CreateMap<StudentAddRequest, Student> ( );

            CreateMap<StudentUpdateRequest, Student> ( );

            CreateMap<StudentResponse, StudentUpdateRequest> ( )
            .ForMember ( dest => dest.BirthDate,
            opt => opt.MapFrom ( src => DateTime.Parse ( src.BirthDateFormatted ) ) );
        }
    }
}