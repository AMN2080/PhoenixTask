using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.WorkSpaces.DeleteWorkSpace;

public sealed record DeleteWorkspaceCommand(Guid WorkSpaceId) : ICommand<Result>;
