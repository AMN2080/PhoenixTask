using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Boards.DeleteBoard;

public sealed record DeleteBoardCommand(Guid BoardId) : ICommand<Result>;
