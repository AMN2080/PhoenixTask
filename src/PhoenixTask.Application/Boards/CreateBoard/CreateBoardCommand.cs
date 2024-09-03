using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Boards.CreateBoard;

public sealed record CreateBoardCommand(Guid ProjectId,string Name ,string Color,int Order) : ICommand<Result<string>>;
