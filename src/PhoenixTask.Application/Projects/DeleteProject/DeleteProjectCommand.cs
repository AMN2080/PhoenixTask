using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Projects.DeleteProject;

public sealed record DeleteProjectCommand(Guid ProjectId) : ICommand<Result>;
internal sealed class DeleteProjectCommandHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IWorkSpaceRepository workSpaceRepository,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteProjectCommand, Result>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var maybeProject =await _projectRepository.GetByIdAsync(request.ProjectId);

        if (maybeProject.HasNoValue)
        {
            return Result.Failure(DomainErrors.Project.NotFound);
        }

        var project = maybeProject.Value;

        #region UserPermitToDeleteProject

        var maybeWorkSpace = await _workSpaceRepository.GetByIdAsync(project.WorkSpaceId);

        if (maybeWorkSpace.Value.OwnerId != _userIdentifierProvider.UserId)
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        #endregion

        _projectRepository.Remove(project);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}