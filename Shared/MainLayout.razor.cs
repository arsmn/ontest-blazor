using System;
using System.Threading.Tasks;
using MudBlazor;
using OnTest.Blazor.Settings;

namespace OnTest.Blazor.Shared
{
    public partial class MainLayout : IDisposable
    {
        private MudTheme _currentTheme;
        private bool _rightToLeft = false;
        private async Task RightToLeftToggle(bool value)
        {
            _rightToLeft = value;
            await Task.CompletedTask;
        }

        protected override async Task OnInitializedAsync()
        {
            _currentTheme = Theme.Default;
            // _currentTheme = await _clientPreferenceManager.GetCurrentThemeAsync();
            // _rightToLeft = await _clientPreferenceManager.IsRTL();
            _interceptorService.RegisterEvent();
        }

        private async Task DarkMode()
        {
            // bool isDarkMode = await _clientPreferenceManager.ToggleDarkModeAsync();
            // _currentTheme = isDarkMode
            //     ? BlazorHeroTheme.DefaultTheme
            //     : BlazorHeroTheme.DarkTheme;
        }

        public void Dispose()
        {
            _interceptorService.DisposeEvent();
        }
    }
}