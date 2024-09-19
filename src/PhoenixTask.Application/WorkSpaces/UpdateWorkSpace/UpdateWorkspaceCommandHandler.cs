using MediatR;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.WorkSpaces.CheckPermission;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.WorkSpaces.UpdateWorkSpace;

internal sealed class UpdateWorkspaceCommandHandler
    (IWorkSpaceRepository workSpaceRepository,
    ISender sender,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateWorkspaceCommand, Result>
{
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;
    public async Task<Result> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var maybeWorkSpace = await _workSpaceRepository.GetByIdAsync(request.WorkSpaceId);

        if (maybeWorkSpace.HasNoValue)
        {
            return Result.Failure(DomainErrors.WorkSpace.NotFound);
        }

        var workSpace = maybeWorkSpace.Value;

        var hasAccess = await _sender.Send(new HasWorkSpacePermissionCommand(workSpace.Id, Domain.Authorities.PermissionType.UpdateWorkSpace));
        if (!hasAccess)
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        var nameResult = Name.Create(request.Name);

        if (nameResult.IsFailure)
        {
            return Result.Failure(nameResult.Error);
        }

        workSpace.Update(nameResult.Value, request.Color);

        _workSpaceRepository.Update(workSpace);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
