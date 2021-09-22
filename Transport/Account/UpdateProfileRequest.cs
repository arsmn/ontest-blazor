using System.ComponentModel.DataAnnotations;

namespace OnTest.Blazor.Transport.Account
{
    public class UpdateProfileRequest
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}