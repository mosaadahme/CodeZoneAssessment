using CodeZone.Core.Common;
using CodeZone.Core.DTOs.Students;

namespace CodeZone.Core.Interfaces
{
    public interface IStudentService
    {
        Task<Result<IEnumerable<StudentResponse>>> GetAllAsync ( );
        Task<Result<PaginatedResult<StudentResponse>>> GetFilteredAsync ( string search, int pageNumber, int pageSize );
        Task<List<string>> GetEnrolledCoursesAsync ( int studentId );
        Task<Result<StudentResponse>> GetByIdAsync ( int id );
        Task<Result> AddAsync ( StudentAddRequest request );
        Task<Result> UpdateAsync ( StudentUpdateRequest request );
        Task<Result> DeleteAsync ( int id );
    }
}