using System;
using System.Collections.Generic;
using System.Text;

namespace CodeZone.Core.Common
{
    public class PaginatedResult<T>
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PaginatedResult ( List<T> data, int count, int pageNumber, int pageSize )
        {
            Data = data;
            TotalCount = count;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling ( count / (double) pageSize );
        }
    }
}
