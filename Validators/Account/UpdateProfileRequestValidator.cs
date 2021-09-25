using System;
using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using OnTest.Blazor.Extensions;
using OnTest.Blazor.Services.Account;
using OnTest.Blazor.Shared.State;
using OnTest.Blazor.Transport.Account;

namespace OnTest.Blazor.Validators.Account
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        private readonly UserState _userState;
        private readonly IAccountService _accountService;

        public UpdateProfileRequestValidator(
            UserState userState,
            IAccountService accountService
        )
        {
            _userState  = userState ??
                throw new ArgumentNullException(nameof(userState));

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
            RuleFor(request => request.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(3).WithMessage("{PropertyName} must be at least of length {MinLength}")
                .MaximumLength(30).WithMessage("{PropertyName} must be at least of length {MaxLength}")
                .Matches(@"^[A-Za-z][A-Za-z0-9_]*$").WithMessage("{PropertyName} must contain only alphabet, number, -, _")
                .MustAsync(async (obj, username, context, cancellation) =>
                {
                    if (string.IsNullOrEmpty(username))
                        return true;

                    if (_userState.Username == username)
                        return true;

                    var result = await _accountService.CheckUsernameAsync(username);
                    if (result.Succeeded)
                        return true;

                    context.MessageFormatter.AppendArgument("CheckErrorMessage", result.Error.Message);
                    return false;
                }).WithMessage("{CheckErrorMessage}");
        }
    }
}