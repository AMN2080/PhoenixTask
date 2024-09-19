using MediatR;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.WorkSpaces.CheckPermission;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.WorkSpaces.DeleteWorkSpace;

internal sealed class DeleteWorkspaceCommandHandler
    (IWorkSpaceRepository workSpaceRepository,
    IUserIdentifierProvider userIdentifierProvider,
    ISender sender,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteWorkspaceCommand, Result>
{
    private readonly ISender _sender= sender;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var maybeWorkSpace = await _workSpaceRepository.GetByIdAsync(request.WorkSpaceId);
        if (maybeWorkSpace.HasNoValue)
        {
            return Result.Failure(DomainErrors.WorkSpace.NotFound);
        }

        var workspace = maybeWorkSpace.Value;

        var hasAccess = await _sender.Send(new HasWorkSpacePermissionCommand(workspace.Id, Domain.Authorities.PermissionType.UpdateWorkSpace));
        if (!hasAccess)
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        _workSpaceRepository.Remove(workspace);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
