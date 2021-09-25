using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnTest.Blazor.Extensions;
using OnTest.Blazor.Shared.State;

namespace OnTest.Blazor.Shared.Components
{
    public partial class UserCard : IDisposable
    {
        [Inject] public UserState UserState { get; set; }
        [Parameter] public UserCardMode Mode { get; set; }
        [Parameter] public string Class { get; set; }

        protected override void OnInitialized()
        {
            UserState.OnChange += StateHasChanged;
        }

        private void Signout()
        {
            var parameters = new DialogParameters
            {
                { nameof(Dialogs.Signout.ContentText), "Are you sure you want to signout now?" },
                { nameof(Dialogs.Signout.ButtonText), "Sign Out" },
                { nameof(Dialogs.Signout.Color), Color.Error },
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            _dialogService.Show<Dialogs.Signout>("Signout", parameters, options);
        }

        public void Dispose()
        {
            UserState.OnChange -= StateHasChanged;
        }
    }

    public enum UserCardMode
    {
        Card,
        Menu
    }
}