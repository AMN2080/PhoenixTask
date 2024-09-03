using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Boards;
using PhoenixTask.Domain.Abstractions.Maybe;

namespace PhoenixTask.Application.Boards.GetBoard;

public sealed record GetBoardQuery(Guid BoardId) : IQuery<Maybe<BoardResult>>;
