using System;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using MudBlazor;
using OnTest.Blazor.Services.Auth;
using OnTest.Blazor.Transport.Auth;

namespace OnTest.Blazor.Pages.Auth
{
    public partial class Signup
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private SignupRequest _model = new();

        protected override async Task OnInitializedAsync()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            if (state.User.Identity.IsAuthenticated)
                _navigationManager.NavigateTo("/");
        }

        private async Task SubmitAsync()
        {
            var result = await _authService.SignupAsync(_model);
            if (result.Succeeded)
            {
                _snackBar.Add("You account has been successfully created!", Severity.Success);
                _navigationManager.NavigateTo("/signin");
                _model = new();
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
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