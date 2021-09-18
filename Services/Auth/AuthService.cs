using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using OnTest.Blazor.Extensions;
using OnTest.Blazor.Transport.Auth;
using OnTest.Blazor.Transport.Shared.Models;
using OnTest.Blazor.Transport.Shared.Wrapper;

namespace OnTest.Blazor.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient ??
                throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IResult> Signin(SigninRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/signin", request);
            var result = await response.ToResult();
            return result;
        }

        public async Task<IResult> Signup(SignupRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/signup", request);
            var result = await response.ToResult();
            return result;
        }

        public async Task<IResult<User>> Whoami()
        {
            var response = await _httpClient.GetAsync("auth/whoami");
            var result = await response.ToResult<User>();
            return result;
        }
    }
}