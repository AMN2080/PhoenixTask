using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.WorkSpaces.UpdateWorkSpace;

public sealed record UpdateWorkspaceCommand(Guid WorkSpaceId, string Name, string Color) : ICommand<Result>;
