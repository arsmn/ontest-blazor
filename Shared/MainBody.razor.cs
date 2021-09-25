using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnTest.Blazor.Extensions;
using OnTest.Blazor.Settings;
using OnTest.Blazor.Shared.State;

namespace OnTest.Blazor.Shared
{
    public partial class MainBody
    {
        [Inject] public UserState UserState { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        private MudTheme _currentTheme;
        private bool _drawerOpen = true;

        protected override void OnInitialized()
        {
            _currentTheme = Theme.Parse(UserState.PreferenceTheme);
            _snackBar.Add($"Welcome {UserState.FullName}", Severity.Success);
        }

        private async Task ToggleTheme()
        {
            string theme = Theme.Rotate(UserState.PreferenceTheme);
            var result = await _accountService.SetPreferenceAsync(new("theme", theme));
            if (result.Succeeded)
            {
                UserState.Preferences["theme"] = theme;
                _currentTheme = Theme.Parse(theme);
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
        }

        private void ToggleDrawer()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}