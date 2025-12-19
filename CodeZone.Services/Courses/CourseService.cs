using AutoMapper;
using CodeZone.Core.DTOs.Courses;
using CodeZone.Core.Entities;
using CodeZone.Core.Interfaces;
using FluentValidation;

namespace CodeZone.Services.Courses
{
    public class CourseService : BaseService<Course, CourseResponse, CourseAddRequest, CourseUpdateRequest>, ICourseService
    {
        public CourseService (
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<CourseAddRequest> addValidator,
            IValidator<CourseUpdateRequest> updateValidator )
            : base ( unitOfWork, mapper, addValidator, updateValidator )
        {
        }
    }
}