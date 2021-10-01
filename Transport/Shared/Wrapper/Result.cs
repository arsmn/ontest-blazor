using System.Collections.Generic;

namespace OnTest.Blazor.Transport.Shared.Wrapper
{
    public class Result : IResult
    {
        public Result() { }

        public Error Error { get; set; }
        public bool Success { get; set; }
        public bool Succeeded => Success && Error is null;
    }

    public class Result<T> : Result, IResult<T>
    {
        public Result() { }

        public T Data { get; set; }
    }

    public class Paginated<T> : Result<List<T>>, IResult<List<T>>
    {
        public Paginated() { }

        public Pager Pager { get; set; }
    }

    public class Pager
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PreviousPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int NextPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}