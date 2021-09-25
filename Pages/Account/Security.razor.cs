using System.Threading.Tasks;
using Blazored.FluentValidation;
using MudBlazor;
using OnTest.Blazor.Transport.Account;

namespace OnTest.Blazor.Pages.Account
{
    public partial class Security
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly ChangePasswordRequest _changeModel = new();
        private readonly SetPasswordRequest _setModel = new();

        private bool _passwordSet = true;
        private bool _processing;

        protected override async Task OnInitializedAsync()
        {
            var result = await _accountService.WhoamiAsync();
            if (result.Succeeded)
                _passwordSet = result.Data.PasswordSet;
            else
                _snackBar.Add(result.Error.Message, MudBlazor.Severity.Error);
        }

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
                _passwordSet = true;
                _setModel.Password = "";
                _setModel.ConfirmPassword = "";
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