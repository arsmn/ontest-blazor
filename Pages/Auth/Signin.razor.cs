using MudBlazor;
using Blazored.FluentValidation;
using OnTest.Blazor.Transport.Auth;
using System.Threading.Tasks;
using OnTest.Blazor.Authentication;
using System;

namespace OnTest.Blazor.Pages.Auth
{
    public partial class Signin
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private SigninRequest _model = new();

        protected override async Task OnInitializedAsync()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            if (state.User.Identity.IsAuthenticated)
                _navigationManager.NavigateTo("/");
        }

        private async Task SubmitAsync()
        {
            var result = await _authService.Signin(_model);
            if (result.Succeeded)
                await (_authenticationStateProvider as HostStateProvider).StateChangedNotifyAsync();
            else
                _snackBar.Add(result.Error.Message, Severity.Error);
        }

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        void TogglePasswordVisibility()
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