using MediatR;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.WorkSpaces.CheckPermission;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;
using WorkSpaceMemberModel = PhoenixTask.Contracts.WorkSpaces.WorkSpaceMember;

namespace PhoenixTask.Application.WorkSpaces.GetWorkSpaceMemberRoles;

internal sealed class GetWorkSpaceMemberRolesQueryHandler(
    IWorkSpaceMemberRepository workSpaceMemberRepository,
    IUserRepository userRepository,
    ISender sender)
    : IQueryHandler<GetWorkSpaceMemberRolesQuery, Maybe<IEnumerable<WorkSpaceMemberModel>>>
{
    private readonly IWorkSpaceMemberRepository _workSpaceMemberRepository = workSpaceMemberRepository;
    private readonly ISender _sender = sender;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Maybe<IEnumerable<WorkSpaceMemberModel>>> Handle(GetWorkSpaceMemberRolesQuery request, CancellationToken cancellationToken)
    {
        var hasAccess = await _sender.Send(new HasWorkSpacePermissionCommand(request.WorkSpaceId, Domain.Authorities.PermissionType.ManageUsers));

        if (!hasAccess)
        {
            return Maybe<IEnumerable<WorkSpaceMemberModel>>.None;
        }

        var users = await _workSpaceMemberRepository.GetWorkSpaceUsers(request.WorkSpaceId);

        if(users.Any())
        {
            return Maybe<IEnumerable<WorkSpaceMemberModel>>.From(users.Select(x => new WorkSpaceMemberModel(x.Id, x.Email)));
        }

        return Maybe<IEnumerable<WorkSpaceMemberModel>>.From([]);
    }
}
