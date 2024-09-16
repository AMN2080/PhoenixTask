using MediatR;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.WorkSpaces.CheckPermission;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.WorkSpaces.DeleteWorkSpaceMember;

internal sealed class DeleteWorkSpaceMemberCommandHandler(
    ISender sender,
    IUserRepository userRepository,
    IUserIdentifierProvider userIdentifierProvider,
    IWorkSpaceMemberRepository workSpaceMemberRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteWorkSpaceMemberCommand, Result>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IWorkSpaceMemberRepository _workSpaceMemberRepository = workSpaceMemberRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ISender _sender = sender;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(DeleteWorkSpaceMemberCommand request, CancellationToken cancellationToken)
    {
        var maybeMember = await _workSpaceMemberRepository.GetMemberByIdAsync(request.WorkSpaceId, request.UserId);

        if (maybeMember.HasNoValue)
        {
            return Result.Failure(DomainErrors.WorkSpaceMember.NotFound);
        }

        var member = maybeMember.Value;

        var maybeCurrentUser = await _userRepository.GetByIdAsync(_userIdentifierProvider.UserId);

        if (maybeCurrentUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        var currentUserHasAccess = await _sender.Send(new HasWorkSpacePermissionCommand(member.WorkSpaceId, PermissionType.ManageAdmin));

        if(!currentUserHasAccess)
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        _workSpaceMemberRepository.Remove(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
