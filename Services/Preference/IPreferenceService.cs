using System.Collections.Generic;
using System.Threading.Tasks;
using MudBlazor;
using OnTest.Blazor.Transport.Shared.Models;
using OnTest.Blazor.Transport.Shared.Wrapper;

namespace OnTest.Blazor.Services.Preference
{
    public interface IPreferenceService : IService
    {
        Task<string> GetPreferenceAsync(string key);
        Task<IResult> SetPreferenceAsync(KeyValuePair<string, string> request);
        Task<MudTheme> GetCurrentThemeAsync();
        Task<MudTheme> ToggleThemeAsync();
    }
}