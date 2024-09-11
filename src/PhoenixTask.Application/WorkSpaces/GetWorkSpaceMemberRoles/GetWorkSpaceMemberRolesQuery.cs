using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Maybe;
using WorkSpaceMemberModel = PhoenixTask.Contracts.WorkSpaces.WorkSpaceMember;

namespace PhoenixTask.Application.WorkSpaces.GetWorkSpaceMemberRoles;

public sealed record GetWorkSpaceMemberRolesQuery(Guid WorkSpaceId) : IQuery<Maybe<IEnumerable<WorkSpaceMemberModel>>>;