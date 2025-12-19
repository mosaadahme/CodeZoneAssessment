using CodeZone.Core.Common;
using CodeZone.Core.DTOs.Students;
using CodeZone.Core.Entities;
using CodeZone.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodeZone.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;

        public GenericRepository ( AppDbContext context )
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync ( int id ) => await _context.Set<T> ( ).FindAsync ( id );
         

        public async Task<IEnumerable<T>> GetAllAsync ( ) => await _context.Set<T> ( ).ToListAsync ( );

        public virtual IQueryable<T> GetTableNoTracking ( ) => _context.Set<T> ( ).AsNoTracking ( ).AsQueryable ( );
         
        public async Task<IEnumerable<T>> FindAsync ( Expression<Func<T, bool>> predicate ) => await _context.Set<T> ( ).Where ( predicate ).ToListAsync ( );
        

        public async Task AddAsync ( T entity ) => await _context.Set<T> ( ).AddAsync ( entity );
         

        public void Update ( T entity ) => _context.Set<T> ( ).Update ( entity );
       

        public void Delete ( T entity ) => _context.Set<T> ( ).Remove ( entity );
        

        public async Task<bool> AnyAsync ( Expression<Func<T, bool>> predicate ) => await _context.Set<T> ( ).AnyAsync ( predicate );

        
    }
}