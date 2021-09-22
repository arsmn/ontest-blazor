using System;
using System.Net;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using MudBlazor;
using OnTest.Blazor.Authentication;
using OnTest.Blazor.Transport.Account;
using Toolbelt.Blazor;

namespace OnTest.Blazor.Pages.Account
{
    public partial class Profile : IDisposable
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly UpdateProfileRequest _model = new();

        private string _avatar;
        private char _firstLetterOfName;

        protected override async Task OnInitializedAsync()
        {
            _httpClientInterceptor.BeforeSend += InterceptorBeforeSend;
            _httpClientInterceptor.AfterSend += InterceptorAfterSend;
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var result = await _accountService.WhoamiAsync();
            if (!result.Succeeded)
            {
                _snackBar.Add(result.Error.Message, MudBlazor.Severity.Error);
                return;
            }

            var user = result.Data;
            _model.Id = user.Id;
            _model.Email = user.Email;
            _model.FirstName = user.FirstName;
            _model.LastName = user.LastName;
            _model.Username = user.Username;
            _avatar = user.Avatar;
            _firstLetterOfName = _model.FirstName.Length > 0 ? _model.FirstName[0] : '-';
            _emailVerified = user.EmailVerified;
            _emailVerifyIcon = user.EmailVerified ? Icons.Material.Filled.Check : Icons.Material.Filled.Warning;
            _emailVerifyColor = user.EmailVerified ? Color.Success : Color.Warning;
        }

        private async Task SubmitAsync()
        {
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

        private string _checkUsernameIcon;
        private Color _checkUsernameColor;

        private void InterceptorBeforeSend(object sender, HttpClientInterceptorEventArgs e)
        {
            if (e.Request.RequestUri.ToString().Contains("check-username"))
            {
                _checkUsernameIcon = Icons.Material.Filled.Sync;
                _checkUsernameColor = Color.Dark;
            }
        }

        private void InterceptorAfterSend(object sender, HttpClientInterceptorEventArgs e)
        {
            if (e.Request.RequestUri.ToString().Contains("check-username"))
            {
                switch (e.Response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        _checkUsernameIcon = Icons.Material.Filled.Check;
                        _checkUsernameColor = Color.Success;
                        break;
                    case HttpStatusCode.Conflict:
                        _checkUsernameIcon = Icons.Material.Filled.Warning;
                        _checkUsernameColor = Color.Error;
                        break;
                }
            }
        }

        public void Dispose()
        {
            _httpClientInterceptor.BeforeSend -= InterceptorBeforeSend;
            _httpClientInterceptor.AfterSend -= InterceptorAfterSend;
        }
    }
}