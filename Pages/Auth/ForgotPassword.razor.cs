using System.Threading.Tasks;
using Blazored.FluentValidation;
using MudBlazor;
using OnTest.Blazor.Transport.Auth;

namespace OnTest.Blazor.Pages.Auth
{
    public partial class ForgotPassword
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly SendResetPasswordRequest _model = new();

        protected override async Task OnInitializedAsync()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            if (state.User.Identity.IsAuthenticated)
                _navigationManager.NavigateTo("/");
        }

        private async Task SubmitAsync()
        {
            var result = await _authService.SendResetPasswordAsync(_model);
            if (result.Succeeded)
            {
                _snackBar.Add("We have sent a reset password email if you have an account.", Severity.Success);
                _navigationManager.NavigateTo("/");
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
        }
    }
}