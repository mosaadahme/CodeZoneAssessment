using CodeZone.Core.Common;
using CodeZone.Core.DTOs.Enrollments;

namespace CodeZone.Core.Interfaces
{
    public interface IEnrollmentService
    {
        Task<Result> EnrollStudentAsync ( EnrollmentRequest request );
        Task<int> GetAvailableSlotsAsync ( int courseId );
    }
}