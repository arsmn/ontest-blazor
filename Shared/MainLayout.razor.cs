using System;
using System.Threading.Tasks;
using MudBlazor;
using OnTest.Blazor.Settings;

namespace OnTest.Blazor.Shared
{
    public partial class MainLayout
    {
        private MudTheme _currentTheme;

        protected override async Task OnInitializedAsync()
        {
            _currentTheme = Theme.Default;
            // _currentTheme = await _clientPreferenceManager.GetCurrentThemeAsync();
            // _rightToLeft = await _clientPreferenceManager.IsRTL();
        }

        private async Task DarkMode()
        {
            // bool isDarkMode = await _clientPreferenceManager.ToggleDarkModeAsync();
            // _currentTheme = isDarkMode
            //     ? BlazorHeroTheme.DefaultTheme
            //     : BlazorHeroTheme.DarkTheme;
        }
    }
}