using CodeZone.Core.Common;
using CodeZone.Core.DTOs.Students;
using CodeZone.Core.Entities;
using System.Linq.Expressions;

namespace CodeZone.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync ( int id );
        Task<IEnumerable<T>> GetAllAsync ( );
        IQueryable<T> GetTableNoTracking ( );
        Task<IEnumerable<T>> FindAsync ( Expression<Func<T, bool>> predicate );
        Task AddAsync ( T entity );
        void Update ( T entity );
        void Delete ( T entity );
        Task<bool> AnyAsync ( Expression<Func<T, bool>> predicate );
    }
}