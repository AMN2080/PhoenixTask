using FluentValidation;
using PhoenixTask.Application.Core.Errors;
using PhoenixTask.Application.Core.Extensions;

namespace PhoenixTask.Application.Boards.UpdateBoard;

internal sealed class UpdateBoardCommandValidation : AbstractValidator<UpdateBoardCommand>
{
    public UpdateBoardCommandValidation()
    {
        RuleFor(e => e.Name).NotEmpty().WithError(ValidationErrors.UpdateBoard.NameIsRequired);

        RuleFor(e => e.Color).NotEmpty().WithError(ValidationErrors.UpdateBoard.ColorIsRequired);
    }
}
