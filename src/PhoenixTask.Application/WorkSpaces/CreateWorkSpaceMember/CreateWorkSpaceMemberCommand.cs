using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.WorkSpaces.AddUserToWorkSpace;

public sealed record CreateWorkSpaceMemberCommand(Guid WorkSpaceId, Guid UserId, int RoleId) : ICommand<Result>;