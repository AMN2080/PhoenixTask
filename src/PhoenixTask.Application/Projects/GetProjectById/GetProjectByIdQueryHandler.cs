using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Projects;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Projects.GetProjectById;

internal sealed class GetProjectByIdQueryHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IProjectRepository projectRepository,
    IWorkSpaceRepository workSpaceRepository) : IQueryHandler<GetProjectByIdQuery, Result<ProjectResult>>
{
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IProjectRepository _projectRepository = projectRepository;
    public async Task<Result<ProjectResult>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var maybeProject = await _projectRepository.GetByIdAsync(request.ProjectId);

        if(maybeProject.HasNoValue)
        {
            return Result.Failure<ProjectResult>(DomainErrors.Project.NotFound);
        }

        var project = maybeProject.Value;

        #region UserPermitToAccessProject

        var maybeWorkspace = await _workSpaceRepository.GetByIdAsync(project.Id);
        if (maybeWorkspace.Value.OwnerId != _userIdentifierProvider.UserId)
        {
            return Result.Failure<ProjectResult>(DomainErrors.User.InvalidPermissions);
        }

        #endregion

        return new ProjectResult(project.Id, project.Name);
    }
}
