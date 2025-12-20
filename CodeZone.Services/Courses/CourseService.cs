using AutoMapper;
using CodeZone.Core.DTOs.Courses;
using CodeZone.Core.Entities;
using CodeZone.Core.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<string>> GetEnrolledStudentsAsync ( int courseId )
        {
            var course = await _repository.GetTableNoTracking ( )
                .Include ( c => c.Enrollments )
                .ThenInclude ( e => e.Student )
                .FirstOrDefaultAsync ( c => c.Id == courseId );

            if ( course == null ) return new List<string> ( );

            return course.Enrollments.Select ( e => e.Student.FullName ).ToList ( );
        }
    }
}