using AutoMapper; 
using CodeZone.Core.Common;
using CodeZone.Core.Entities;
using CodeZone.Core.Interfaces;
using FluentValidation;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
namespace CodeZone.Services
{
    public abstract class BaseService<TEntity, TResponse, TAddReq, TUpdateReq>
        where TEntity : BaseEntity
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper; 
        protected readonly IValidator<TAddReq> _addValidator;
        protected readonly IValidator<TUpdateReq> _updateValidator;
        protected readonly IGenericRepository<TEntity> _repository;
        public BaseService (
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<TAddReq> addValidator = null,
            IValidator<TUpdateReq> updateValidator = null )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _addValidator = addValidator;
            _updateValidator = updateValidator; 
            _repository = _unitOfWork.Repository<TEntity> ( );
        }

        public virtual async Task<Result<IEnumerable<TResponse>>> GetAllAsync ( )
        {
            var entities = await _unitOfWork.Repository<TEntity> ( ).GetAllAsync ( );
            var response = _mapper.Map<IEnumerable<TResponse>> ( entities );
            return Result<IEnumerable<TResponse>>.Success ( response );
        }


        public virtual async Task<Result<PaginatedResult<TResponse>>> GetPaginatedListAsync (
    Expression<Func<TEntity, bool>> filter = null,
    int pageNumber = 1,
    int pageSize = 10 )
        {
            try
            {
                var query = _repository.GetTableNoTracking ( );

                if ( filter != null )
                {
                    query = query.Where ( filter );
                }

                var totalCount = await query.CountAsync ( );

                var data = await query
                    .Skip ( ( pageNumber - 1 ) * pageSize )
                    .Take ( pageSize )
                    .ToListAsync ( );

                var mappedData = _mapper.Map<List<TResponse>> ( data );

                var result = new PaginatedResult<TResponse> ( mappedData, totalCount, pageNumber, pageSize );

                return Result<PaginatedResult<TResponse>>.Success ( result );
            }
            catch ( Exception ex )
            {
                return Result<PaginatedResult<TResponse>>.Failure ( $"Error retrieving data: {ex.Message}" );
            }
        }

        public virtual async Task<Result<TResponse>> GetByIdAsync ( int id )
        {
            var entity = await _unitOfWork.Repository<TEntity> ( ).GetByIdAsync ( id );
            if ( entity == null ) return Result<TResponse>.Failure ( "Record not found" );

            return Result<TResponse>.Success ( _mapper.Map<TResponse> ( entity ) );
        }

        public virtual async Task<Result> AddAsync ( TAddReq request )
        {
            if ( _addValidator != null )
            {
                var validationResult = await _addValidator.ValidateAsync ( request );
                if ( !validationResult.IsValid )
                    return Result.Failure ( string.Join ( ", ", validationResult.Errors.Select ( e => e.ErrorMessage ) ) );
            }

            var entity = _mapper.Map<TEntity> ( request );

            await _unitOfWork.Repository<TEntity> ( ).AddAsync ( entity );
            await _unitOfWork.CompleteAsync ( );

            return Result.Success ( );
        }

        public virtual async Task<Result> UpdateAsync ( TUpdateReq request )
        {
            var idProp = typeof ( TUpdateReq ).GetProperty ( "Id" );
            if ( idProp == null ) return Result.Failure ( "Update DTO must contain an 'Id' property." );

            var id = (int) idProp.GetValue ( request );

            if ( _updateValidator != null )
            {
                var validationResult = await _updateValidator.ValidateAsync ( request );
                if ( !validationResult.IsValid )
                    return Result.Failure ( string.Join ( ", ", validationResult.Errors.Select ( e => e.ErrorMessage ) ) );
            }

            var entity = await _unitOfWork.Repository<TEntity> ( ).GetByIdAsync ( id );
            if ( entity == null ) return Result.Failure ( "Record not found" );

            _mapper.Map ( request, entity );

            _unitOfWork.Repository<TEntity> ( ).Update ( entity );
            await _unitOfWork.CompleteAsync ( );

            return Result.Success ( );
        }

        public virtual async Task<Result> DeleteAsync ( int id )
        {
            var entity = await _unitOfWork.Repository<TEntity> ( ).GetByIdAsync ( id );
            if ( entity == null ) return Result.Failure ( "Record not found" );

            _unitOfWork.Repository<TEntity> ( ).Delete ( entity );
            await _unitOfWork.CompleteAsync ( );
            return Result.Success ( );
        }
    }
}