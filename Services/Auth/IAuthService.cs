using System.Security.Claims;
using System.Threading.Tasks;
using OnTest.Blazor.Transport.Auth;
using OnTest.Blazor.Transport.Shared.Models;
using OnTest.Blazor.Transport.Shared.Wrapper;

namespace OnTest.Blazor.Services.Auth
{
    public interface IAuthService : IService
    {
        Task<IResult> Signin(SigninRequest request);
        Task<IResult> Signup(SignupRequest request);
        Task<IResult> Signout();
        Task<IResult<User>> Whoami();
    }
}