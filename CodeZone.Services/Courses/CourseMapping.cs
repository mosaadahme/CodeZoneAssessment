using AutoMapper;
using CodeZone.Core.DTOs.Courses;
using CodeZone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeZone.Services.Courses
{
    public class CourseMapping: Profile
    {
        public CourseMapping ( )
        {
            CreateMap<Course, CourseResponse> ( );

            CreateMap<CourseAddRequest, Course> ( );

            CreateMap<CourseUpdateRequest, Course> ( );

            CreateMap<CourseResponse, CourseUpdateRequest> ( );

            CreateMap<Course, CourseResponse> ( )
    .ForMember ( dest => dest.EnrollmentsCount, opt => opt.MapFrom ( src => src.Enrollments.Count ) );
        }
    }
}
