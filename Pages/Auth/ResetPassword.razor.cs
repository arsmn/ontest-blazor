using System.Threading.Tasks;
using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnTest.Blazor.Transport.Auth;

namespace OnTest.Blazor.Pages.Auth
{
    public partial class ResetPassword
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly ResetPasswordRequest _model = new();

        [Parameter] public string Code { get; set; }

        private bool _processing;

        protected override void OnParametersSet()
        {
            if (Code.Length <= 30)
                _navigationManager.NavigateTo("/");

            _model.Code = Code;
        }

        protected override async Task OnInitializedAsync()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            if (state.User.Identity.IsAuthenticated)
                _navigationManager.NavigateTo("/");
        }

        private async Task SubmitAsync()
        {
            _processing = true;
            var result = await _authService.ResetPasswordAsync(_model);
            if (result.Succeeded)
            {
                _snackBar.Add("Your password reset successfully. Please sign in", Severity.Success);
                _navigationManager.NavigateTo("/");
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _processing = false;
        }


        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        private void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }
    }
}