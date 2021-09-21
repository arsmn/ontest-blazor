using FluentValidation;
using OnTest.Blazor.Transport.Auth;

namespace OnTest.Blazor.Validators.Auth
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator()
        {
            RuleFor(request => request.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("{PropertyName} is not correct");
        }
    }
}