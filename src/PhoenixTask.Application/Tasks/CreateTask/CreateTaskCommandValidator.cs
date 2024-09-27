using FluentValidation;
using PhoenixTask.Application.Core.Errors;
using PhoenixTask.Application.Core.Extensions;

namespace PhoenixTask.Application.Tasks.CreateTask;

internal sealed class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithError(ValidationErrors.CreateTask.DescriptionIsRequierd);
    }
}
