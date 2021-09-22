
namespace OnTest.Blazor.Transport.Auth
{
    public class SigninRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}