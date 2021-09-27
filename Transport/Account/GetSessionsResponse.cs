using System.Collections.Generic;
using OnTest.Blazor.Transport.Shared.Models;

namespace OnTest.Blazor.Transport.Account
{
    public class GetSessionsResponse
    {
        public GetSessionsResponse()
        {
            Others = new List<Session>();
        }

        public Session Current { get; set; }
        public List<Session> Others { get; set; }
    }
}