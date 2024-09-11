using MediatR;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.WorkSpaces.CheckPermission;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Workspaces;
using WorkSpaceMemberModel = PhoenixTask.Contracts.WorkSpaces.WorkSpaceMember;

namespace PhoenixTask.Application.WorkSpaces.GetWorkSpaceMemberRoles;

internal sealed class GetWorkSpaceMemberRolesQueryHandler(
    IWorkSpaceMemberRepository workSpaceMemberRepository,
    ISender sender)
    : IQueryHandler<GetWorkSpaceMemberRolesQuery, Maybe<IEnumerable<WorkSpaceMemberModel>>>
{
    private readonly IWorkSpaceMemberRepository _workSpaceMemberRepository = workSpaceMemberRepository;
    private readonly ISender _sender = sender;

    public async Task<Maybe<IEnumerable<WorkSpaceMemberModel>>> Handle(GetWorkSpaceMemberRolesQuery request, CancellationToken cancellationToken)
    {
        var hasAccess = await _sender.Send(new HasWorkSpacePermissionCommand(request.WorkSpaceId, Domain.Authorities.PermissionType.ManageUsers));

        if (!hasAccess)
        {
            return Maybe<IEnumerable<WorkSpaceMemberModel>>.None;
        }

        var members = await _workSpaceMemberRepository.GetMembersByIdAsync(request.WorkSpaceId);

        var result = members.Select(e =>
        new WorkSpaceMemberModel(e.User.Id,
        e.User.Email,
        e.Roles.Select(x => x.Value).ToArray()
        ));

        return Maybe<IEnumerable<WorkSpaceMemberModel>>.From(result);
    }
}
