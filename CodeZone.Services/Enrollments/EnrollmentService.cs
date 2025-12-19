using CodeZone.Core.Common;
using CodeZone.Core.DTOs.Enrollments;
using CodeZone.Core.Entities;
using CodeZone.Core.Interfaces;
using FluentValidation;

namespace CodeZone.Services.Enrollments
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<EnrollmentRequest> _validator;

        public EnrollmentService ( IUnitOfWork unitOfWork, IValidator<EnrollmentRequest> validator )
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> EnrollStudentAsync ( EnrollmentRequest request )
        {
            var validationResult = await _validator.ValidateAsync ( request );
            if ( !validationResult.IsValid )
                return Result.Failure ( validationResult.Errors.First ( ).ErrorMessage );

            var student = await _unitOfWork.Repository<Student> ( ).GetByIdAsync ( request.StudentId );
            if ( student == null ) return Result.Failure ( "Student not found." );

            var course = await _unitOfWork.Repository<Course> ( ).GetByIdAsync ( request.CourseId );
            if ( course == null ) return Result.Failure ( "Course not found." );

            var isEnrolled = await _unitOfWork.Repository<Enrollment> ( )
                .AnyAsync ( e => e.StudentId == request.StudentId && e.CourseId == request.CourseId );
            if ( isEnrolled ) return Result.Failure ( "Student is already enrolled in this course." );

            var currentCount = ( await _unitOfWork.Repository<Enrollment> ( )
                .FindAsync ( e => e.CourseId == request.CourseId ) ).Count ( );

            if ( currentCount >= course.MaxCapacity )
                return Result.Failure ( "Course is fully booked." );

             var enrollment = new Enrollment
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId
            };

            await _unitOfWork.Repository<Enrollment> ( ).AddAsync ( enrollment );
            await _unitOfWork.CompleteAsync ( );

            return Result.Success ( );
        }

        public async Task<int> GetAvailableSlotsAsync ( int courseId )
        {
            var course = await _unitOfWork.Repository<Course> ( ).GetByIdAsync ( courseId );
            if ( course == null ) return 0;

            var currentCount = ( await _unitOfWork.Repository<Enrollment> ( )
                .FindAsync ( e => e.CourseId == courseId ) ).Count ( );

            return Math.Max ( 0, course.MaxCapacity - currentCount );
        }
    }
}