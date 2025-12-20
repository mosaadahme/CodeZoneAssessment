using AutoMapper;
using CodeZone.Core.Common;
using CodeZone.Core.DTOs.Students;
using CodeZone.Core.Entities;
using CodeZone.Core.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodeZone.Services.Students
{
    public class StudentService : BaseService<Student, StudentResponse, StudentAddRequest, StudentUpdateRequest>, IStudentService
    {
        public StudentService (
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<StudentAddRequest> addValidator,
            IValidator<StudentUpdateRequest> updateValidator )
            : base ( unitOfWork, mapper, addValidator, updateValidator )
        {
        }

         public override async Task<Result> AddAsync ( StudentAddRequest request )
        {
            var emailExists = await _unitOfWork.Repository<Student> ( ).AnyAsync ( s => s.Email == request.Email );
            if ( emailExists ) return Result.Failure ( "Email already exists." );

            return await base.AddAsync ( request );
        }


        public async Task<Result<PaginatedResult<StudentResponse>>> GetFilteredAsync ( string search, int pageNumber, int pageSize )
        {
            Expression<Func<Student, bool>> filter = null;
            if ( !string.IsNullOrEmpty ( search ) )
            {
                search = search.Trim().ToLower ( );
                filter = x => x.FullName.ToLower ( ).Contains ( search )
                           || x.Email.ToLower ( ).Contains ( search )
                           || x.NationalId.Contains ( search );
            }

            return await GetPaginatedListAsync ( filter, pageNumber, pageSize );
        }

        public async Task<List<string>> GetEnrolledCoursesAsync ( int studentId )
        {
            var student = await _repository.GetTableNoTracking ( )
                .Include ( s => s.Enrollments )
                .ThenInclude ( e => e.Course )
                .FirstOrDefaultAsync ( s => s.Id == studentId );

            if ( student == null ) return new List<string> ( );

            return student.Enrollments.Select ( e => e.Course.Title ).ToList ( );
        }

        public override async Task<Result> UpdateAsync ( StudentUpdateRequest request )
        {
            var emailExists = await _unitOfWork.Repository<Student> ( )
                .AnyAsync ( s => s.Email == request.Email && s.Id != request.Id );

            if ( emailExists ) return Result.Failure ( "Email is taken." );

            return await base.UpdateAsync ( request );
        }
    }
}