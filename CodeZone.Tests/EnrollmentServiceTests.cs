using CodeZone.Core.DTOs.Enrollments;
using CodeZone.Core.Entities;
using CodeZone.Core.Interfaces;
using CodeZone.Services.Enrollments;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;
using System.Linq.Expressions;

namespace CodeZone.Tests
{
    public class EnrollmentServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IGenericRepository<Student>> _mockStudentRepo;
        private readonly Mock<IGenericRepository<Course>> _mockCourseRepo;
        private readonly Mock<IGenericRepository<Enrollment>> _mockEnrollmentRepo;

        private readonly Mock<IValidator<EnrollmentRequest>> _mockValidator;

        private readonly EnrollmentService _enrollmentService;

        public EnrollmentServiceTests ( )
        {
            _mockUnitOfWork = new Mock<IUnitOfWork> ( );
            _mockStudentRepo = new Mock<IGenericRepository<Student>> ( );
            _mockCourseRepo = new Mock<IGenericRepository<Course>> ( );
            _mockEnrollmentRepo = new Mock<IGenericRepository<Enrollment>> ( );

            _mockValidator = new Mock<IValidator<EnrollmentRequest>> ( );

            _mockValidator.Setup ( v => v.ValidateAsync ( It.IsAny<EnrollmentRequest> ( ), It.IsAny<CancellationToken> ( ) ) )
                          .ReturnsAsync ( new ValidationResult ( ) );

            _mockUnitOfWork.Setup ( u => u.Repository<Student> ( ) ).Returns ( _mockStudentRepo.Object );
            _mockUnitOfWork.Setup ( u => u.Repository<Course> ( ) ).Returns ( _mockCourseRepo.Object );
            _mockUnitOfWork.Setup ( u => u.Repository<Enrollment> ( ) ).Returns ( _mockEnrollmentRepo.Object );

            _enrollmentService = new EnrollmentService ( _mockUnitOfWork.Object, _mockValidator.Object );
        }

        [Fact]
        public async Task EnrollStudentAsync_ShouldReturnSuccess_WhenValidRequest ( )
        {
            var studentId = 1;
            var courseId = 101;
            var request = new EnrollmentRequest { StudentId = studentId, CourseId = courseId };

            var student = new Student { Id = studentId, FullName = "Test Student" };
            var course = new Course { Id = courseId, Title = "C# Course", MaxCapacity = 10, Enrollments = new List<Enrollment> ( ) };

            _mockStudentRepo.Setup ( r => r.GetByIdAsync ( studentId ) ).ReturnsAsync ( student );
            _mockCourseRepo.Setup ( r => r.GetByIdAsync ( courseId ) ).ReturnsAsync ( course );

            _mockEnrollmentRepo.Setup ( r => r.AnyAsync ( It.IsAny<Expression<Func<Enrollment, bool>>> ( ) ) )
                               .ReturnsAsync ( false );

            var result = await _enrollmentService.EnrollStudentAsync ( request );

            Assert.True ( result.IsSuccess );

            _mockEnrollmentRepo.Verify ( r => r.AddAsync ( It.IsAny<Enrollment> ( ) ), Times.Once ( ) );
            _mockUnitOfWork.Verify ( u => u.CompleteAsync ( ), Times.Once ( ) );
        }

        [Fact]
        public async Task EnrollStudentAsync_ShouldReturnFailure_WhenCourseIsFull ( )
        {
            var studentId = 1;
            var courseId = 102;
            var request = new EnrollmentRequest { StudentId = studentId, CourseId = courseId };

            var fullEnrollments = Enumerable.Range ( 1, 10 ).Select ( i => new Enrollment ( ) ).ToList ( );
            var course = new Course { Id = courseId, Title = "Full Course", MaxCapacity = 10, Enrollments = fullEnrollments };

            _mockCourseRepo.Setup ( r => r.GetByIdAsync ( courseId ) ).ReturnsAsync ( course );


            var result = await _enrollmentService.EnrollStudentAsync ( request );

            Assert.False ( result.IsSuccess );
            Assert.Equal ( "Course is full.", result.ErrorMessage );

            _mockEnrollmentRepo.Verify ( r => r.AddAsync ( It.IsAny<Enrollment> ( ) ), Times.Never ( ) );
        }
    }
}