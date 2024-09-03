using FluentValidation;
using PhoenixTask.Application.Core.Errors;
using PhoenixTask.Application.Core.Extensions;

namespace PhoenixTask.Application.Boards.CreateBoard;

internal sealed class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
{
    public CreateBoardCommandValidator()
    {
        RuleFor(e => e.Name).NotEmpty().WithError(ValidationErrors.CreateBoard.NameIsRequired);

        RuleFor(e => e.Color).NotEmpty().WithError(ValidationErrors.CreateBoard.ColorIsRequired);
    }
}