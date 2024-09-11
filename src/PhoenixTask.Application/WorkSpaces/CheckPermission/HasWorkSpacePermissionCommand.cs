using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Authorities;

namespace PhoenixTask.Application.WorkSpaces.CheckPermission;

public sealed record HasWorkSpacePermissionCommand(Guid WorkSpaceId, PermissionType Permission) : ICommand<bool>;
