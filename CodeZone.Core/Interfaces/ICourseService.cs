using CodeZone.Core.Common;
using CodeZone.Core.DTOs.Courses;

namespace CodeZone.Core.Interfaces
{
    public interface ICourseService
    {
        Task<Result<IEnumerable<CourseResponse>>> GetAllAsync ( );
        Task<Result<CourseResponse>> GetByIdAsync ( int id );
        Task<Result> AddAsync ( CourseAddRequest request );
        Task<Result> UpdateAsync ( CourseUpdateRequest request );
        Task<Result> DeleteAsync ( int id );
    }
}