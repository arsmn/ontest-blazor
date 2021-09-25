using System.Collections.Generic;
using OnTest.Blazor.Transport.Shared.Models;

namespace OnTest.Blazor.Shared.State
{
    public class UserState : StateContainer
    {
        private long id;
        public long Id
        {
            get => id;
            set { SetValue(ref id, value); }
        }

        private string email;
        public string Email
        {
            get => email;
            set { SetValue(ref email, value); }
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set { SetValue(ref firstName, value); }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set { SetValue(ref lastName, value); }
        }

        private string username;
        public string Username
        {
            get => username;
            set { SetValue(ref username, value); }
        }

        private string avatar;
        public string Avatar
        {
            get => avatar;
            set { SetValue(ref avatar, value); }
        }

        public string FullName => $"{this.FirstName} {this.LastName}";

        private bool emailVerified;
        public bool EmailVerified
        {
            get => emailVerified;
            set { SetValue(ref emailVerified, value); }
        }

        private bool passwordSet;
        public bool PasswordSet
        {
            get => passwordSet;
            set { SetValue(ref passwordSet, value); }
        }

        private IDictionary<string, string> preferences;
        public IDictionary<string, string> Preferences
        {
            get => preferences;
            set { SetValue(ref preferences, value); }
        }

        public char FirstLetterOfName => FirstName.Length > 0 ? FirstName[0] : '-';
        public string PreferenceTheme => !string.IsNullOrEmpty(Preferences?["theme"]) ? Preferences["theme"] : "default";

        public void SetModel(User user)
        {
            this.Id = user.Id;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Username = user.Username;
            this.Avatar = user.Avatar;
            this.EmailVerified = user.EmailVerified;
            this.PasswordSet = user.PasswordSet;
            this.Preferences = user.Preferences;
        }
    }
}