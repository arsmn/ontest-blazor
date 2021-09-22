using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OnTest.Blazor.Extensions;
using OnTest.Blazor.Transport.Account;
using OnTest.Blazor.Transport.Shared.Models;
using OnTest.Blazor.Transport.Shared.Wrapper;

namespace OnTest.Blazor.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient ??
                throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IResult> UpdateProfileAsync(UpdateProfileRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync("account", request, JsonExtensions.Options);
            var result = await response.ToResult();
            return result;
        }

        public async Task<IResult<User>> WhoamiAsync()
        {
            var response = await _httpClient.GetAsync("account/whoami");
            var result = await response.ToResult<User>();
            return result;
        }

        public async Task<IResult> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("account/change-password", request, JsonExtensions.Options);
            var result = await response.ToResult();
            return result;
        }

        public async Task<IResult> CheckUsernameAsync(string username)
        {
            return await CheckAsync("username", username);
        }

        public async Task<IResult> CheckEmailAsync(string email)
        {
            return await CheckAsync("email", email);
        }

        private async Task<IResult> CheckAsync(string type, string value)
        {
            var response = await _httpClient.GetAsync($"account/check/{type}/{value}");
            var result = await response.ToResult();
            return result;
        }

        public async Task<IResult> SendVerificationAsync()
        {
            var response = await _httpClient.PostAsync("account/send/verification", null);
            var result = await response.ToResult();
            return result;
        }

        public async Task<IResult> VerificationAsync(VerificationRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("account/verification", request, JsonExtensions.Options);
            var result = await response.ToResult();
            return result;
        }
    }
}