using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using OnTest.Blazor.Services.Auth;
using OnTest.Blazor.Transport.Auth;

namespace OnTest.Blazor.Authentication
{
    public class HostStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _authService;

        public HostStateProvider(IAuthService authService)
        {
            _authService = authService ??
                throw new ArgumentNullException(nameof(authService));
        }

        public async Task StateChangedNotifyAsync()
        {
            var authState = Task.FromResult(await GetAuthenticationStateAsync());
            NotifyAuthenticationStateChanged(authState);
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState(await FetchSignedInUser());
        }

        private async Task<ClaimsPrincipal> FetchSignedInUser()
        {
            var result = await _authService.Whoami();
            if (result.Succeeded)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, result.Data.Email),
                    new Claim(ClaimTypes.Name, result.Data.FirstName),
                    new Claim(ClaimTypes.Surname, result.Data.LastName),
                    new Claim(ClaimTypes.GivenName, result.Data.FullName),
                    new Claim(ClaimTypes.NameIdentifier, result.Data.Username)
                };
                return new ClaimsPrincipal(new ClaimsIdentity(claims, "cookie"));
            }
            else
            {
                switch (result.Error.Code)
                {
                    case (int)HttpStatusCode.Unauthorized:
                        return new ClaimsPrincipal(new ClaimsIdentity());
                    default:
                        throw new HttpRequestException(null, null, (HttpStatusCode?)result.Error.Code);
                }
            }
        }
    }
}