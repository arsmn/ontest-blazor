using System.Text.Json.Serialization;

namespace OnTest.Blazor.Transport.Shared.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName => FirstName + " " + LastName;
    }
}