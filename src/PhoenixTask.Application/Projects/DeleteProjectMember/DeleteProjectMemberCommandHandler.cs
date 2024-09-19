using MediatR;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.WorkSpaces.CheckPermission;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;

namespace PhoenixTask.Application.Projects.DeleteProjectMember;

internal sealed class DeleteProjectMemberCommandHandler(
    IProjectMemberRepository projectMemberRepository,
    ISender sender,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<DeleteProjectMemberCommand, Result>
{
    private readonly IProjectMemberRepository _projectMemberRepository = projectMemberRepository;
    private readonly ISender _sender = sender;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var maybeMember = await _projectMemberRepository.GetMemberByIdAsync(request.ProjectId, request.UserId);

        if (maybeMember.HasNoValue)
        {
            return Result.Failure(DomainErrors.ProjectMember.NotFound);
        }

        var member = maybeMember.Value;

        var currentUserHasAccess = await _sender.Send(new HasWorkSpacePermissionCommand(member.ProjectId, PermissionType.ManageAdmin));

        if (!currentUserHasAccess)
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        _projectMemberRepository.Remove(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
