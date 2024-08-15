using FluentValidation;
using PhoenixTask.Application.Core.Errors;
using PhoenixTask.Application.Core.Extensions;

namespace PhoenixTask.Application.WorkSpaces.UpdateWorkSpace;

internal sealed class UpdateWorkspaceCommandValidation : AbstractValidator<UpdateWorkspaceCommand>
{
    public UpdateWorkspaceCommandValidation()
    {
        RuleFor(e=>e.WorkSpaceId).NotEmpty().WithError(ValidationErrors.UpdateWorkSpace.WorkSpaceIdIsRequired);

        RuleFor(e => e.Color).NotEmpty().WithError(ValidationErrors.UpdateWorkSpace.ColorIsRequired);

        RuleFor(e => e.Name).NotEmpty().WithError(ValidationErrors.UpdateWorkSpace.NameIsRequired);
    }
}