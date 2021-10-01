using System.Collections.Generic;

namespace OnTest.Blazor.Transport.Shared.Wrapper
{
    public interface IResult
    {
        public Error Error { get; set; }
        public bool Success { get; set; }
        public bool Succeeded { get; }
    }

    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }
}