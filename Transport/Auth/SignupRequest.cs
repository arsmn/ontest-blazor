
using System.Text.Json.Serialization;

namespace OnTest.Blazor.Transport.Auth
{
    public class SignupRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public string ConfirmPassword { get; set; }
    }
}