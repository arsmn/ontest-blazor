using System;
using FluentValidation;
using OnTest.Blazor.Services.Account;
using OnTest.Blazor.Transport.Auth;

namespace OnTest.Blazor.Validators.Auth
{
    public class SignupRequestValidator : AbstractValidator<SignupRequest>
    {
        private readonly IAccountService _accountService;

        public SignupRequestValidator(IAccountService accountService)
        {
            _accountService = accountService ??
                throw new ArgumentNullException(nameof(accountService));

            RuleFor(request => request.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(3).WithMessage("{PropertyName} must be at least of length {MinLength}")
                .MaximumLength(50).WithMessage("{PropertyName} must be at least of length {MaxLength}");
            RuleFor(request => request.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(3).WithMessage("{PropertyName} must be at least of length {MinLength}")
                .MaximumLength(50).WithMessage("{PropertyName} must be at least of length {MaxLength}");
            RuleFor(request => request.Password)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(5).WithMessage("{PropertyName} must be at least of length {MinLength}")
                .MaximumLength(50).WithMessage("{PropertyName} must be at least of length {MaxLength}");
            RuleFor(request => request.ConfirmPassword)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Equal(request => request.Password).WithMessage("Passwords don't match");
            RuleFor(request => request.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("{PropertyName} is not correct")
                .MustAsync(async (obj, username, context, cancellation) =>
                {
                    if (string.IsNullOrEmpty(username))
                        return true;

                    var result = await _accountService.CheckEmailAsync(username);
                    if (result.Succeeded)
                        return true;

                    context.MessageFormatter.AppendArgument("CheckErrorMessage", result.Error.Message);
                    return false;
                }).WithMessage("{CheckErrorMessage}");
        }
    }
}