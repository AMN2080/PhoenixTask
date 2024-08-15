using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.WorkSpaces;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.WorkSpaces.GetWorkSpaceById;

internal sealed class WorkspaceByIdQueryHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IWorkSpaceRepository workSpaceRepository)
    : IQueryHandler<WorkspaceByIdQuery, Maybe<WorkSpaceResult>>
{
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    public async Task<Maybe<WorkSpaceResult>> Handle(WorkspaceByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _userIdentifierProvider.UserId;

        var maybeWorkSpace = await _workSpaceRepository.GetByIdAsync(request.WorkSpaceId);

        if (maybeWorkSpace.HasNoValue)
        {
            return Maybe<WorkSpaceResult>.None;
        }

        var workspace = maybeWorkSpace.Value;

        if (workspace.OwnerId != userId)
        {
            return Maybe<WorkSpaceResult>.None;
        }

        return new WorkSpaceResult(workspace.Id, workspace.Name, workspace.Color);
    }
}
