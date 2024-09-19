using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Application.Projects.CreateProjectMember;

internal sealed class CreateProjectMemberCommandHandler(
    IProjectRepository projectRepository,
    IProjectMemberRepository projectMemberRepository
,
    IUserIdentifierProvider userIdentifierProvider,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateProjectMemberCommand, Result>
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IProjectMemberRepository _projectMemberRepository = projectMemberRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(CreateProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var maybeProject = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (maybeProject.HasNoValue)
        {
            return Result.Failure(DomainErrors.Project.NotFound);
        }

        var hasAccess = await _projectMemberRepository.UserHasAccess(request.ProjectId, _userIdentifierProvider.UserId, PermissionType.ManageAdmin);

        if (!hasAccess || _userIdentifierProvider.UserId.Equals(request.UserId))
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        var maybeRole = Role.FromValue(request.RoleId);

        if (maybeRole.HasNoValue)
        {
            return Result.Failure(DomainErrors.Role.NotFound);
        }
        var maybeUser = await _userRepository.GetByIdAsync(request.UserId);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        var userAlreadyHasRole = await _projectMemberRepository.AnyAsync(maybeProject.Value.Id, maybeUser.Value.Id, maybeRole.Value.Value);

        if (userAlreadyHasRole)
        {
            return Result.Success();
        }

        var member = ProjectMember.Create(maybeProject.Value, maybeUser.Value, maybeRole.Value);

        _projectMemberRepository.Insert(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
