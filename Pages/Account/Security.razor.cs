using System.Threading.Tasks;
using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnTest.Blazor.Authentication;
using OnTest.Blazor.Extensions;
using OnTest.Blazor.Shared.State;
using OnTest.Blazor.Transport.Account;

namespace OnTest.Blazor.Pages.Account
{
    public partial class Security
    {
        [Inject] public UserState UserState { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly ChangePasswordRequest _changeModel = new();
        private readonly SetPasswordRequest _setModel = new();

        private bool _processing;


        private async Task SubmitChangePasswordAsync()
        {
            _processing = true;
            var result = await _accountService.ChangePasswordAsync(_changeModel);
            if (result.Succeeded)
            {
                _changeModel.Terminate = false;
                _changeModel.NewPassword = "";
                _changeModel.CurrentPassword = "";
                _changeModel.ConfirmNewPassword = "";
                _snackBar.Add("Password Changed!", Severity.Success);
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _processing = false;
        }

        private async Task SubmitSetPasswordAsync()
        {
            _processing = true;
            var result = await _accountService.SetPasswordAsync(_setModel);
            if (result.Succeeded)
            {
                _setModel.Password = "";
                _setModel.ConfirmPassword = "";
                UserState.PasswordSet = true;
                _snackBar.Add("Password Set!", Severity.Success);
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _processing = false;
        }

        private bool _currentPasswordVisibility;
        private InputType _currentPasswordInput = InputType.Password;
        private string _currentPasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        private void ToggleCurrentPasswordVisibility()
        {
            if (_currentPasswordVisibility)
            {
                _currentPasswordVisibility = false;
                _currentPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                _currentPasswordInput = InputType.Password;
            }
            else
            {
                _currentPasswordVisibility = true;
                _currentPasswordInputIcon = Icons.Material.Filled.Visibility;
                _currentPasswordInput = InputType.Text;
            }
        }

        private bool _newPasswordVisibility;
        private InputType _newPasswordInput = InputType.Password;
        private string _newPasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        private void ToggleNewPasswordVisibility()
        {
            if (_newPasswordVisibility)
            {
                _newPasswordVisibility = false;
                _newPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                _newPasswordInput = InputType.Password;
            }
            else
            {
                _newPasswordVisibility = true;
                _newPasswordInputIcon = Icons.Material.Filled.Visibility;
                _newPasswordInput = InputType.Text;
            }
        }
    }
}