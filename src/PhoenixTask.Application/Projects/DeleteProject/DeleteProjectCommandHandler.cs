using MediatR;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.Projects.CheckPermission;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Projects.DeleteProject;

internal sealed class DeleteProjectCommandHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IWorkSpaceRepository workSpaceRepository,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork,
    ISender sender) : ICommandHandler<DeleteProjectCommand, Result>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ISender _sender = sender;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var maybeProject = await _projectRepository.GetByIdAsync(request.ProjectId);

        if (maybeProject.HasNoValue)
        {
            return Result.Failure(DomainErrors.Project.NotFound);
        }

        var project = maybeProject.Value;

        #region UserPermitToDeleteProject

        var hasAccess = await _sender.Send(new HasProjectPermissionCommand(project.Id, Domain.Authorities.PermissionType.DeleteProject));

        if (!hasAccess)
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        #endregion

        _projectRepository.Remove(project);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}