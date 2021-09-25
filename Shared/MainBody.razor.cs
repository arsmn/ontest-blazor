using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnTest.Blazor.Extensions;

namespace OnTest.Blazor.Shared
{
    public partial class MainBody
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        // [Parameter] public EventCallback OnToggleTheme { get; set; }

        private MudTheme _currentTheme;
        private bool _drawerOpen = true;

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("OnInitializedAsync");
            _currentTheme = await _preferenceService.GetCurrentThemeAsync();
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            _snackBar.Add($"Welcome {state.User.GetFullName()}", Severity.Success);
        }

        private async Task ToggleTheme()
        {
            _currentTheme = await _preferenceService.ToggleThemeAsync();
        }

        private void ToggleDrawer()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}