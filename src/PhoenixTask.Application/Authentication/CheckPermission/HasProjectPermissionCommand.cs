using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Authorities;

namespace PhoenixTask.Application.Authentication.CheckPermission;

public sealed record HasProjectPermissionCommand(Guid ProjectId, PermissionType Permission) : ICommand<bool>;
