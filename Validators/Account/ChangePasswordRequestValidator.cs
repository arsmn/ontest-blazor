using FluentValidation;
using OnTest.Blazor.Transport.Account;

namespace OnTest.Blazor.Validators.Account
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(request => request.CurrentPassword)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(request => request.NewPassword)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(5).WithMessage("{PropertyName} must be at least of length {MinLength}")
                .MaximumLength(50).WithMessage("{PropertyName} must be at least of length {MaxLength}");
            RuleFor(request => request.ConfirmNewPassword)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Equal(request => request.NewPassword).WithMessage("Passwords don't match");
        }
    }
}