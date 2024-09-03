using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Boards;

namespace PhoenixTask.Application.Boards.GetBoardsByProject;

public sealed record GetBoardByProjectQuery(Guid ProjectId) : IQuery<IEnumerable<BoardResult>>;
