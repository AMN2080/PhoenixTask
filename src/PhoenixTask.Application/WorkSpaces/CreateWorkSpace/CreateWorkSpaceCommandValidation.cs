using FluentValidation;
using PhoenixTask.Application.Core.Errors;
using PhoenixTask.Application.Core.Extensions;

namespace PhoenixTask.Application.WorkSpaces.CreateWorkSpace;

internal sealed class CreateWorkSpaceCommandValidation : AbstractValidator<CreateWorkSpaceCommand>
{
    public CreateWorkSpaceCommandValidation()
    {
        RuleFor(e => e.Name).NotEmpty().WithError(ValidationErrors.CreateWorkSpace.NameIsRequired);

        RuleFor(e=>e.Color).NotEmpty().WithError(ValidationErrors.CreateWorkSpace.ColorIsRequired);
    }
}