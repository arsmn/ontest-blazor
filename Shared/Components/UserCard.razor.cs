using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnTest.Blazor.Extensions;

namespace OnTest.Blazor.Shared.Components
{
    public partial class UserCard
    {
        [Parameter] public UserCardMode Mode { get; set; } = UserCardMode.Card;
        [Parameter] public string Class { get; set; }
        [Parameter] public string ImageUrl { get; set; }

        private string Id { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Email { get; set; }
        private char FirstLetterOfName { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = state.User;

            this.Id = user.GetId();
            this.Email = user.GetEmail();
            this.FirstName = user.GetFirstName();
            this.LastName = user.GetLastName();
            this.FirstLetterOfName = this.FirstName.Length > 0 ? FirstName[0] : '-';

            StateHasChanged();
        }

        private void Signout()
        {
            var parameters = new DialogParameters
            {
                { nameof(Dialogs.Signout.ContentText), "Are you sure you want to logout now?" },
                { nameof(Dialogs.Signout.ButtonText), "Signout" },
                { nameof(Dialogs.Signout.Color), Color.Error },
                { nameof(Dialogs.Signout.CurrentUserId), this.Id }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            _dialogService.Show<Dialogs.Signout>("Signout", parameters, options);
        }
    }

    public enum UserCardMode
    {
        Card,
        Menu
    }
}