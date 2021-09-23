using System.Security.Claims;

namespace OnTest.Blazor.Extensions
{
    internal static class ClaimsPrincipalExtensions
    {
        internal const string ClaimTypeAvatar = "claims.avatar";

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
    }
}