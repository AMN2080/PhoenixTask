using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Boards.UpdateBoard;

public sealed record UpdateBoardCommand(Guid BoardId, string Name, int Order, string Color) : ICommand<Result>;
