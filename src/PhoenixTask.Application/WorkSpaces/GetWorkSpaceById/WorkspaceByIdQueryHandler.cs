using MediatR;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.WorkSpaces.CheckPermission;
using PhoenixTask.Contracts.WorkSpaces;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.WorkSpaces.GetWorkSpaceById;

internal sealed class WorkspaceByIdQueryHandler(
    IWorkSpaceRepository workSpaceRepository,
    ISender sender)
    : IQueryHandler<WorkspaceByIdQuery, Maybe<WorkSpaceResult>>
{
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly ISender _sender = sender;
    public async Task<Maybe<WorkSpaceResult>> Handle(WorkspaceByIdQuery request, CancellationToken cancellationToken)
    {
        var maybeWorkSpace = await _workSpaceRepository.GetByIdAsync(request.WorkSpaceId);

        if (maybeWorkSpace.HasNoValue)
        {
            return Maybe<WorkSpaceResult>.None;
        }

        var workspace = maybeWorkSpace.Value;

        var hasAccess = await _sender.Send(new HasWorkSpacePermissionCommand(workspace.Id, Domain.Authorities.PermissionType.ReadProject));
        if (!hasAccess)
        {
            return Maybe<WorkSpaceResult>.None;
        }

        return new WorkSpaceResult(workspace.Id, workspace.Name, workspace.Color);
    }
}
