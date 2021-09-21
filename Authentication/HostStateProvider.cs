using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using LazyCache;
using Microsoft.AspNetCore.Components.Authorization;
using OnTest.Blazor.Services.Auth;
using OnTest.Blazor.Transport.Auth;

namespace OnTest.Blazor.Authentication
{
    public class HostStateProvider : AuthenticationStateProvider
    {
        private const string CacheKey = "Auth-State";
        private readonly IAppCache _appCache;
        private readonly IAuthService _authService;

        public HostStateProvider(
            IAppCache appCache,
            IAuthService authService
        )
        {
            _appCache = appCache ??
                throw new ArgumentNullException(nameof(appCache));

            _authService = authService ??
                throw new ArgumentNullException(nameof(authService));
        }

        public async Task StateChangedNotifyAsync()
        {
            _appCache.Remove(CacheKey);
            var authState = Task.FromResult(await GetAuthenticationStateAsync());
            NotifyAuthenticationStateChanged(authState);
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            Func<Task<ClaimsPrincipal>> UserGetter = async () => await this.FetchSignedInUser();
            var user = await _appCache.GetOrAddAsync(CacheKey, UserGetter);

            return new AuthenticationState(user);
        }

        private async Task<ClaimsPrincipal> FetchSignedInUser()
        {
            var result = await _authService.WhoamiAsync();
            if (result.Succeeded)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, result.Data.Email ?? ""),
                    new Claim(ClaimTypes.Name, result.Data.FirstName ?? ""),
                    new Claim(ClaimTypes.Surname, result.Data.LastName ?? ""),
                    new Claim(ClaimTypes.GivenName, result.Data.FullName ?? ""),
                    new Claim(ClaimTypes.NameIdentifier, result.Data.Username ?? "")
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