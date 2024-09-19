using MediatR;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.Projects.CheckPermission;
using PhoenixTask.Contracts.Projects;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Projects.GetProjectById;

internal sealed class GetProjectByIdQueryHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IProjectRepository projectRepository,
    IWorkSpaceRepository workSpaceRepository,
    ISender sender) : IQueryHandler<GetProjectByIdQuery, Result<ProjectResult>>
{
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ISender _sender = sender;
    public async Task<Result<ProjectResult>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var maybeProject = await _projectRepository.GetByIdAsync(request.ProjectId);

        if (maybeProject.HasNoValue)
        {
            return Result.Failure<ProjectResult>(DomainErrors.Project.NotFound);
        }

        var project = maybeProject.Value;

        #region UserPermitToAccessProject

        var hasAccess = await _sender.Send(new HasProjectPermissionCommand(project.Id, Domain.Authorities.PermissionType.ReadProject));

        if (!hasAccess)
        {
            return Result.Failure<ProjectResult>(DomainErrors.User.InvalidPermissions);
        }

        #endregion

        return new ProjectResult(project.Id, project.Name);
    }
}
