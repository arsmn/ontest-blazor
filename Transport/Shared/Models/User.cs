using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OnTest.Blazor.Transport.Shared.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public bool EmailVerified { get; set; }
        public bool PasswordSet { get; set; }
        public string FullName { get; set; }
        public IDictionary<string, string> Preferences { get; set; }

        public char FirstLetterOfName => FirstName.Length > 0 ? FirstName[0] : '-';
        public string PreferenceTheme => !string.IsNullOrEmpty(Preferences?["theme"]) ? Preferences["theme"] : "default";
    }
}