using System.Collections.Generic;

namespace OnTest.Blazor.Transport.Shared.Wrapper
{
    public class Error
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Request { get; set; }
        public string Reason { get; set; }
        public Dictionary<string, string> Details { get; set; }
        public string Message { get; set; }
    }
}