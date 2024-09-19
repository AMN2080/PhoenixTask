using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Projects.CreateProjectMember;

public sealed record CreateProjectMemberCommand(Guid ProjectId, Guid UserId, int RoleId) : ICommand<Result>;
