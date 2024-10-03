using FluentValidation;
using PhoenixTask.Application.Core.Errors;
using PhoenixTask.Application.Core.Extensions;

namespace PhoenixTask.Application.Users.UpdateUser;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty()
            .When(e => IsNullOrEmpty(e.LastName, e.PhoneNumber))
            .WithError(ValidationErrors.UpdateUser.HaveAtLeastOnePropertyNotNull);

        RuleFor(e => e.LastName)
            .NotEmpty()
            .When(e => IsNullOrEmpty(e.FirstName, e.PhoneNumber))
            .WithError(ValidationErrors.UpdateUser.HaveAtLeastOnePropertyNotNull);

        RuleFor(e => e.PhoneNumber)
            .NotEmpty()
            .When(e => IsNullOrEmpty(e.FirstName, e.LastName))
            .WithError(ValidationErrors.UpdateUser.HaveAtLeastOnePropertyNotNull);
    }
    bool IsNullOrEmpty(params string?[] properies)
    {
        return properies.All(p=>string.IsNullOrEmpty(p));
    }
}