using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using OnTest.Blazor.Authentication;
using OnTest.Blazor.Extensions;
using OnTest.Blazor.Transport.Account;
using Toolbelt.Blazor;

namespace OnTest.Blazor.Pages.Account
{
    public partial class Profile
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly UpdateProfileRequest _model = new();

        private bool _processing;
        private char _firstLetterOfName;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = state.User.GetUser();

            _model.Id = user.Id;
            _model.Email = user.Email;
            _model.FirstName = user.FirstName;
            _model.LastName = user.LastName;
            _model.Username = user.Username;

            _firstLetterOfName = _model.FirstName.Length > 0 ? _model.FirstName[0] : '-';
            _emailVerified = user.EmailVerified;
            _emailVerifyIcon = user.EmailVerified ? Icons.Material.Filled.Check : Icons.Material.Filled.Warning;
            _emailVerifyColor = user.EmailVerified ? Color.Success : Color.Warning;
            _userAvatar = user.Avatar;
            _cardAvatar = user.Avatar;
        }

        private async Task SubmitAsync()
        {
            _processing = true;
            var result = await _accountService.UpdateProfileAsync(_model);
            if (result.Succeeded)
            {
                _snackBar.Add("Your profile updated", Severity.Success);
                await (_authenticationStateProvider as HostStateProvider).StateChangedNotifyAsync();
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _processing = false;
        }

        private bool _emailVerified;
        private string _emailVerifyIcon;
        private Color _emailVerifyColor;

        private async Task SendEmailVerification()
        {
            if (_emailVerified)
            {
                _snackBar.Add("Your email is verified!", Severity.Info);
            }
            else
            {
                var parameters = new DialogParameters
                {
                    { nameof(Shared.Dialogs.Confirmation.Title), "Confirmation email" },
                    { nameof(Shared.Dialogs.Confirmation.Content), "Do you want to send confirmation email?" },
                    { nameof(Shared.Dialogs.Confirmation.Icon), Icons.Material.Filled.Help },
                    { nameof(Shared.Dialogs.Confirmation.Color), Color.Primary },
                };
                var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
                var dialog = await _dialogService.Show<Shared.Dialogs.Confirmation>("Sure", parameters, options).Result;
                if (!dialog.Cancelled)
                {
                    var result = await _accountService.SendVerificationAsync();
                    if (result.Succeeded)
                    {
                        _snackBar.Add("We have sent an email. Please check your inbox and spam folder.", Severity.Info);
                    }
                    else
                    {
                        _snackBar.Add(result.Error.Message, Severity.Error);
                    }
                }
            }
        }

        private string _tmpAvatar;
        private string _userAvatar;
        private string _cardAvatar;
        private bool _avatarProcessing;
        private IBrowserFile _avatarFile;

        private async Task SelectAvatar(InputFileChangeEventArgs args)
        {
            if (args.FileCount == 0)
            {
                _snackBar.Add("Please select an image", Severity.Warning);
                return;
            }

            _avatarFile = args.File;
            byte[] buffer = new byte[_avatarFile.Size];
            await _avatarFile.OpenReadStream(10485760).ReadAsync(buffer);
            _tmpAvatar = $"data:{_avatarFile.ContentType};base64,{Convert.ToBase64String(buffer)}";
            _cardAvatar = _tmpAvatar;
        }

        private async Task UploadAvatar()
        {
            if (_avatarFile is null)
            {
                _snackBar.Add("Please select an image", Severity.Warning);
                return;
            }

            _avatarProcessing = true;

            using var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(_avatarFile.OpenReadStream(10485760));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(_avatarFile.ContentType);
            content.Add(
                content: fileContent,
                name: "\"file\"",
                fileName: _avatarFile.Name);
            var result = await _accountService.SetAvatarAsync(content);
            if (result.Succeeded)
            {
                _userAvatar = _tmpAvatar;
                _tmpAvatar = string.Empty;
                _snackBar.Add("Avatar changed!", Severity.Success);
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _avatarProcessing = false;
        }

        private async Task GenerateAvatar()
        {
            _avatarProcessing = true;
            var result = await _accountService.GenerateAvatarAsync();
            if (result.Succeeded)
            {
                _tmpAvatar = string.Empty;

                // change hash to reload image
                string avatar = $"{result.Data.Avatar}?hash={Guid.NewGuid().ToString()}";
                _cardAvatar = avatar;
                _userAvatar = avatar;
                _snackBar.Add("Avatar changed!", Severity.Success);
                StateHasChanged();
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _avatarProcessing = false;
        }

        private async Task DeleteAvatar()
        {
            _avatarProcessing = true;
            var result = await _accountService.DeleteAvatarAsync();
            if (result.Succeeded)
            {
                _tmpAvatar = string.Empty;
                _userAvatar = string.Empty;
                _cardAvatar = string.Empty;
                _snackBar.Add("Avatar deleted!", Severity.Success);
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _avatarProcessing = false;
        }

        private void ClearAvatar()
        {
            _tmpAvatar = string.Empty;
            _cardAvatar = _userAvatar;
        }
    }
}