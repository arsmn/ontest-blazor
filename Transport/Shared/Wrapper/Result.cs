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
}