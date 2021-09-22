using System;
using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using OnTest.Blazor.Extensions;
using OnTest.Blazor.Services.Account;
using OnTest.Blazor.Transport.Account;

namespace OnTest.Blazor.Validators.Account
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        private readonly IAccountService _accountService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UpdateProfileRequestValidator(
            IAccountService accountService,
            AuthenticationStateProvider authenticationStateProvider
        )
        {
            _accountService = accountService ??
                throw new ArgumentNullException(nameof(accountService));

            _authenticationStateProvider = authenticationStateProvider ??
                throw new ArgumentNullException(nameof(authenticationStateProvider));

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

                    var user = await _authenticationStateProvider.GetAuthenticationStateAsync();
                    if (user.User.GetUsername() == username)
                        return true;

                    var result = await _accountService.CheckUsernameAsync(username);
                    if (result.Succeeded)
                        return true;

                    context.MessageFormatter.AppendArgument("UsernameErrorMessage", result.Error.Message);
                    return false;
                }).WithMessage("{UsernameErrorMessage}");
        }
    }
}