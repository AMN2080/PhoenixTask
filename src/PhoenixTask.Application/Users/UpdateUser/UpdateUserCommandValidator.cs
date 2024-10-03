using FluentValidation;
using PhoenixTask.Application.Core.Errors;
using PhoenixTask.Application.Core.Extensions;

namespace PhoenixTask.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(e => e)
            .Must(HaveAtLeastOnePropertyNotNull).WithError(ValidationErrors.UpdateUser.HaveAtLeastOnePropertyNotNull);
    }

    private bool HaveAtLeastOnePropertyNotNull(UpdateUserCommand command)
    {
        return command.FirstName != null || command.LastName != null || command.PhoneNumber != null;
    }
}