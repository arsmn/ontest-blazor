using System.Net.Http;
using System.Threading.Tasks;
using OnTest.Blazor.Transport.Account;
using OnTest.Blazor.Transport.Shared.Models;
using OnTest.Blazor.Transport.Shared.Wrapper;

namespace OnTest.Blazor.Services.Account
{
    public interface IAccountService : IService
    {
        Task<IResult<User>> WhoamiAsync();
        Task<IResult> ChangePasswordAsync(ChangePasswordRequest request);
        Task<IResult> SetPasswordAsync(SetPasswordRequest request);
        Task<IResult<User>> UpdateProfileAsync(UpdateProfileRequest request);
        Task<IResult> CheckUsernameAsync(string username);
        Task<IResult> CheckEmailAsync(string email);
        Task<IResult> SendVerificationAsync();
        Task<IResult> VerificationAsync(VerificationRequest request);
        Task<IResult<User>> SetAvatarAsync(MultipartFormDataContent content);
        Task<IResult<User>> GenerateAvatarAsync();
        Task<IResult<User>> DeleteAvatarAsync();
    }
}