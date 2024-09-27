using FluentValidation;
using PhoenixTask.Application.Core.Errors;
using PhoenixTask.Application.Core.Extensions;

namespace PhoenixTask.Application.Tasks.UpdateTask;

internal sealed class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithError(ValidationErrors.UpdateTask.DescriptionIsRequierd);
    }
}
