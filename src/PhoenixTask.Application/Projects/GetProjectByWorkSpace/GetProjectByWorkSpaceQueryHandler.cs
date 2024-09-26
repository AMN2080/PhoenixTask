using MediatR;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.WorkSpaces.CheckPermission;
using PhoenixTask.Contracts.Projects;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Projects.GetProjectByWorkSpace;

internal sealed class GetProjectByWorkSpaceQueryHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IWorkSpaceRepository workSpaceRepository,
    IProjectRepository projectRepository,
    ISender sender) : IQueryHandler<GetProjectByWorkSpaceQuery, IEnumerable<ProjectResult>>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ISender _sender =sender;
    public async Task<IEnumerable<ProjectResult>> Handle(GetProjectByWorkSpaceQuery request, CancellationToken cancellationToken)
    {
        var maybeWorkSpace = await _workSpaceRepository.GetByIdAsync(request.WorkSpaceId);

        if (maybeWorkSpace.HasNoValue)
        {
            return [];
        }

        #region UserPermitToGetProjects

        var hasAccess = await _sender.Send(new HasWorkSpacePermissionCommand(maybeWorkSpace.Value.Id, Domain.Authorities.PermissionType.ReadProject));
        #endregion

        var projects = await _projectRepository.GetAllProjectsByWorkSpaceIdAsync(request.WorkSpaceId);

        return projects.Select(x => new ProjectResult(x.Id, x.Name));
    }
}