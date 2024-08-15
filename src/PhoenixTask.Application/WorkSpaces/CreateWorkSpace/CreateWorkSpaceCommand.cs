using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.WorkSpaces.CreateWorkSpace;

public sealed record CreateWorkSpaceCommand(string Name, string Color) : ICommand<Result<Guid>>;
