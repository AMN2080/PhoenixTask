using FluentValidation;
using PhoenixTask.Application.Core.Errors;
using PhoenixTask.Application.Core.Extensions;

namespace PhoenixTask.Application.Authentication.Login;

public sealed class LoginWithUserNameCommandValidator : AbstractValidator<LoginWithUserNameCommand>
{
    public LoginWithUserNameCommandValidator()
    {
        RuleFor(e => e.Username).NotEmpty().WithError(ValidationErrors.Login.UsernameIsRequired);

        RuleFor(e => e.Password).NotEmpty().WithError(ValidationErrors.Login.PasswordIsRequired);
    }
}