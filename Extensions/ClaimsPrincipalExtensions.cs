using System.Security.Claims;
using OnTest.Blazor.Transport.Shared.Models;

namespace OnTest.Blazor.Extensions
{
    internal static class ClaimsPrincipalExtensions
    {
        internal const string ClaimTypeAvatar = "claims.avatar";
        internal const string ClaimTypeFullName = "claims.fullname";
        internal const string ClaimTypePasswordSet = "claims.passwordset";
        internal const string ClaimTypeEmailVerified = "claims.emailverified";
        internal const string ClaimTypePreferenceTheme = "claims.preference.theme";

        internal static long GetId(this ClaimsPrincipal claimsPrincipal)
            => long.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));

        internal static string GetEmail(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypes.Email);

        internal static string GetUsername(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypes.Name);

        internal static string GetFirstName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypes.GivenName);

        internal static string GetLastName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypes.Surname);

        internal static string GetAvatar(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypeAvatar);

        internal static string GetFullName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypeFullName);

        internal static bool GetPasswordSet(this ClaimsPrincipal claimsPrincipal)
            => bool.Parse(claimsPrincipal.FindFirstValue(ClaimTypePasswordSet));

        internal static bool GetEmailVerified(this ClaimsPrincipal claimsPrincipal)
            => bool.Parse(claimsPrincipal.FindFirstValue(ClaimTypeEmailVerified));

        internal static string GetStringValue(this ClaimsPrincipal claimsPrincipal, string key)
            => claimsPrincipal.FindFirstValue(key);

        internal static User GetUser(this ClaimsPrincipal claimsPrincipal)
            => new User
            {
                Id = claimsPrincipal.GetId(),
                Email = claimsPrincipal.GetEmail(),
                FirstName = claimsPrincipal.GetFirstName(),
                LastName = claimsPrincipal.GetLastName(),
                Username = claimsPrincipal.GetUsername(),
                Avatar = claimsPrincipal.GetAvatar(),
                EmailVerified = claimsPrincipal.GetEmailVerified(),
                PasswordSet = claimsPrincipal.GetPasswordSet(),
                FullName = claimsPrincipal.GetFullName(),
            };
    }
}