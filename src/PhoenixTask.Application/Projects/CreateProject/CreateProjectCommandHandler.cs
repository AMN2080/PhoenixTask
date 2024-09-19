using MediatR;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.Projects.CheckPermission;
using PhoenixTask.Application.WorkSpaces.CheckPermission;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Projects.CreateProject;

internal sealed class CreateProjectCommandHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IWorkSpaceRepository workSpaceRepository,
    IProjectRepository projectRepository,
    ISender sender,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateProjectCommand, Result<string>>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ISender _sender = sender;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<string>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var nameResult = Name.Create(request.Name);

        if (nameResult.IsFailure)
        {
            return Result.Failure<string>(nameResult.Error);
        }

        var mayBeWorkSpace = await _workSpaceRepository.GetByIdAsync(request.WorkSpaceId);
        if (mayBeWorkSpace.HasNoValue)
        {
            return Result.Failure<string>(DomainErrors.WorkSpace.NotFound);
        }

        var workSpace = mayBeWorkSpace.Value;

        #region UserPermitToCreateProject

        var hasAccess = await _sender.Send(new HasWorkSpacePermissionCommand(request.WorkSpaceId, Domain.Authorities.PermissionType.CreateProject));

        if (!hasAccess)
        {
            return Result.Failure<string>(DomainErrors.User.InvalidPermissions);
        }

        #endregion

        var project = Project.Create(workSpace, nameResult.Value);

        _projectRepository.Insert(project);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return project.Id.ToString();
    }
}