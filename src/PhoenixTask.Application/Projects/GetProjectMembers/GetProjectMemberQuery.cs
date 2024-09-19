using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Maybe;
using ProjectMemberModel = PhoenixTask.Contracts.Projects.ProjectMember;

namespace PhoenixTask.Application.Projects.GetProjectMembers;

public sealed record GetProjectMemberQuery(Guid ProjectId) : IQuery<Maybe<IEnumerable<ProjectMemberModel>>>;
