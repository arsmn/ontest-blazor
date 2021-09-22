using System.Security.Claims;
using System.Threading.Tasks;
using OnTest.Blazor.Transport.Auth;
using OnTest.Blazor.Transport.Shared.Models;
using OnTest.Blazor.Transport.Shared.Wrapper;

namespace OnTest.Blazor.Services.Auth
{
    public interface IAuthService : IService
    {
        Task<IResult> SigninAsync(SigninRequest request);
        Task<IResult> SignupAsync(SignupRequest request);
        Task<IResult> SignoutAsync();
        Task<IResult> SendResetPasswordAsync(SendResetPasswordRequest request);
        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);
    }
}