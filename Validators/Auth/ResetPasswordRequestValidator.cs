using FluentValidation;
using OnTest.Blazor.Transport.Auth;

namespace OnTest.Blazor.Validators.Auth
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(request => request.Code)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(request => request.Password)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(5).WithMessage("{PropertyName} must be at least of length {MinLength}")
                .MaximumLength(50).WithMessage("{PropertyName} must be at least of length {MaxLength}");
            RuleFor(request => request.ConfirmPassword)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Equal(request => request.Password).WithMessage("Passwords don't match");
        }
    }
}