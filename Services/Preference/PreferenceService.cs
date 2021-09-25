using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using OnTest.Blazor.Extensions;
using OnTest.Blazor.Settings;
using OnTest.Blazor.Transport.Shared.Wrapper;

namespace OnTest.Blazor.Services.Preference
{
    public class PreferenceService : IPreferenceService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public PreferenceService(
            HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider
        )
        {
            _httpClient = httpClient ??
                throw new ArgumentNullException(nameof(httpClient));

            _authenticationStateProvider = authenticationStateProvider ??
                throw new ArgumentNullException(nameof(authenticationStateProvider));
        }

        public async Task<MudTheme> GetCurrentThemeAsync()
        {
            return Theme.Parse(await GetPreferenceAsync("theme"));
        }

        public async Task<MudTheme> ToggleThemeAsync()
        {
            string current = await GetPreferenceAsync("theme");
            string theme = Theme.Rotate(current);
            await SetPreferenceAsync(new("theme", theme));
            return Theme.Parse(theme);
        }

        public async Task<string> GetPreferenceAsync(string key)
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return state.User.GetPreference(key);
        }

        public async Task<IResult> SetPreferenceAsync(KeyValuePair<string, string> request)
        {
            var response = await _httpClient.PostAsJsonAsync("account/set-preference", request, JsonExtensions.Options);
            var result = await response.ToResult();
            return result;
        }
    }
}