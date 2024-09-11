using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Authorities;

namespace PhoenixTask.Application.Projects.CheckPermission;

public sealed record HasProjectPermissionCommand(Guid ProjectId, PermissionType Permission) : ICommand<bool>;
