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
        [Parameter] public EventCallback OnToggleTheme { get; set; }

        private bool _drawerOpen = true;

        protected override async Task OnInitializedAsync()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            _snackBar.Add($"Welcome {state.User.GetFirstName()} {state.User.GetLastName()}", Severity.Success);
        }

        public async Task ToggleTheme()
        {
            await OnToggleTheme.InvokeAsync();
        }

        private void ToggleDrawer()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}