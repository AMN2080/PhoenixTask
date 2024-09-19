using MediatR;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.Projects.CheckPermission;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Projects.UpdateProject;

internal sealed class UpdateProjectCommandHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IWorkSpaceRepository workSpaceRepository,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork,
    ISender sender) : ICommandHandler<UpdateProjectCommand, Result>
{
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var nameResult = Name.Create(request.Name);

        if (nameResult.IsFailure)
        {
            return Result.Failure(nameResult.Error);
        }

        var mayBeproject = await _projectRepository.GetByIdAsync(request.ProjectId);

        if (mayBeproject.HasNoValue)
        {
            return Result.Failure(DomainErrors.Project.NotFound);
        }

        var project = mayBeproject.Value;

        #region UserPermitToUpdateProject

        var hasAccess = await _sender.Send(new HasProjectPermissionCommand(project.Id, Domain.Authorities.PermissionType.UpdateProject));

        if (!hasAccess)
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        #endregion

        project.UpdateName(nameResult.Value);

        _projectRepository.Update(project);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
