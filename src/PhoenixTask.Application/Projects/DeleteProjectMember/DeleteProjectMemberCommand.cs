using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Projects.DeleteProjectMember;

public sealed record DeleteProjectMemberCommand(Guid ProjectId, Guid UserId) : ICommand<Result>;
