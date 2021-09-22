using System.Text.Json.Serialization;

namespace OnTest.Blazor.Transport.Account
{
    public class SetPasswordRequest
    {
        public string Password { get; set; }

        [JsonIgnore]
        public string ConfirmPassword { get; set; }
    }
}