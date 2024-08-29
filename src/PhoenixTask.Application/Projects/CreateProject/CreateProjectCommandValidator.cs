using FluentValidation;
using PhoenixTask.Application.Core.Errors;
using PhoenixTask.Application.Core.Extensions;

namespace PhoenixTask.Application.Projects.CreateProject;

internal sealed class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(e => e.Name).NotEmpty().WithError(ValidationErrors.CreateProject.NameIsRequired);

        RuleFor(e => e.WorkSpaceId).NotEmpty().WithError(ValidationErrors.CreateProject.WorkSpaceIdIsRequired);
    }
}