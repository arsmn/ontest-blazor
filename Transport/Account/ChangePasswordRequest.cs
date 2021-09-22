using System.Text.Json.Serialization;

namespace OnTest.Blazor.Transport.Account
{
    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        [JsonIgnore]
        public string ConfirmNewPassword { get; set; }
        public bool Terminate { get; set; }
    }
}