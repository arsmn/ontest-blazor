using FluentValidation;
using OnTest.Blazor.Transport.Auth;

namespace OnTest.Blazor.Validators.Auth
{
    public class SendResetPasswordRequestValidator : AbstractValidator<SendResetPasswordRequest>
    {
        public SendResetPasswordRequestValidator()
        {
            RuleFor(request => request.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("{PropertyName} is not correct");
        }
    }
}