namespace OnTest.Blazor.Transport.Auth
{
    public class ResetPasswordRequest
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
}