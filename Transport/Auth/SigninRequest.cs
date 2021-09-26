
namespace OnTest.Blazor.Transport.Auth
{
    public class SigninRequest
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}