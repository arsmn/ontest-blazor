using FluentValidation;
using OnTest.Blazor.Transport.Auth;

namespace OnTest.Blazor.Validators.Auth
{
    public class SigninRequestValidator : AbstractValidator<SigninRequest>
    {
        public SigninRequestValidator()
        {
            RuleFor(request => request.Identifier)
                .NotEmpty().WithMessage("Username or Email is required");
            RuleFor(request => request.Password)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}