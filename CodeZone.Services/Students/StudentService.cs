using AutoMapper;
using CodeZone.Core.Common;
using CodeZone.Core.DTOs.Students;
using CodeZone.Core.Entities;
using CodeZone.Core.Interfaces;
using FluentValidation;

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

         public override async Task<Result> UpdateAsync ( StudentUpdateRequest request )
        {
            var emailExists = await _unitOfWork.Repository<Student> ( )
                .AnyAsync ( s => s.Email == request.Email && s.Id != request.Id );

            if ( emailExists ) return Result.Failure ( "Email is taken." );

            return await base.UpdateAsync ( request );
        }
    }
}