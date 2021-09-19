using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace OnTest.Blazor.Shared
{
    public partial class MainBody
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback OnDarkModeToggle { get; set; }

        private bool _drawerOpen = true;

        private string CurrentUserId { get; set; }
        private string ImageDataUrl { get; set; }
        private string FirstName { get; set; }
        private string SecondName { get; set; }
        private string Email { get; set; }
        private char FirstLetterOfName { get; set; }

        public async Task ToggleDarkMode()
        {
            await OnDarkModeToggle.InvokeAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            // _rightToLeft = await _clientPreferenceManager.IsRTL();

            // _snackBar.Add(string.Format(_localizer["Welcome {0}"], FirstName), Severity.Success);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // if (firstRender)
            // {
            //     await LoadDataAsync();
            // }
        }

        private async Task LoadDataAsync()
        {
            // var state = await _stateProvider.GetAuthenticationStateAsync();
            // var user = state.User;
            // if (user == null) return;
            // if (user.Identity?.IsAuthenticated == true)
            // {
            //     if (string.IsNullOrEmpty(CurrentUserId))
            //     {
            //         CurrentUserId = user.GetUserId();
            //         FirstName = user.GetFirstName();
            //         if (FirstName.Length > 0)
            //         {
            //             FirstLetterOfName = FirstName[0];
            //         }

            //         SecondName = user.GetLastName();
            //         Email = user.GetEmail();
            //         var imageResponse = await _accountManager.GetProfilePictureAsync(CurrentUserId);
            //         if (imageResponse.Succeeded)
            //         {
            //             ImageDataUrl = imageResponse.Data;
            //         }

            //         var currentUserResult = await _userManager.GetAsync(CurrentUserId);
            //         if (!currentUserResult.Succeeded || currentUserResult.Data == null)
            //         {
            //             _snackBar.Add(
            //                 _localizer["You are logged out because the user with your Token has been deleted."],
            //                 Severity.Error);
            //             CurrentUserId = string.Empty;
            //             ImageDataUrl = string.Empty;
            //             FirstName = string.Empty;
            //             SecondName = string.Empty;
            //             Email = string.Empty;
            //             FirstLetterOfName = char.MinValue;
            //             await _authenticationManager.Logout();
            //         }
            //     }
            // }
        }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private void Logout()
        {
            // var parameters = new DialogParameters
            // {
            //     {nameof(Dialogs.Logout.ContentText), $"{_localizer["Logout Confirmation"]}"},
            //     {nameof(Dialogs.Logout.ButtonText), $"{_localizer["Logout"]}"},
            //     {nameof(Dialogs.Logout.Color), Color.Error},
            //     {nameof(Dialogs.Logout.CurrentUserId), CurrentUserId},
            //     {nameof(Dialogs.Logout.HubConnection), hubConnection}
            // };

            // var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            // _dialogService.Show<Dialogs.Logout>(_localizer["Logout"], parameters, options);
        }
    }
}