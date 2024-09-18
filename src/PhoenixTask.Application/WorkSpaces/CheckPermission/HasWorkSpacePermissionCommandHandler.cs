using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.WorkSpaces.CheckPermission;

internal sealed class HasWorkSpacePermissionCommandHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IWorkSpaceMemberRepository workSpaceMemberRepository
    ) : ICommandHandler<HasWorkSpacePermissionCommand, bool>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IWorkSpaceMemberRepository _workSpaceMemberRepository = workSpaceMemberRepository;
    public async Task<bool> Handle(HasWorkSpacePermissionCommand request, CancellationToken cancellationToken) 
        => await _workSpaceMemberRepository
        .UserHasAccess(request.WorkSpaceId,
            _userIdentifierProvider.UserId,
            request.Permission);

}