using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Projects;

namespace PhoenixTask.Application.Projects.CheckPermission;

internal sealed class HasProjectPermissionCommandHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IProjectMemberRepository projectMemberRepository) : ICommandHandler<HasProjectPermissionCommand, bool>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IProjectMemberRepository _projectMemberRepository = projectMemberRepository;
    public async Task<bool> Handle(HasProjectPermissionCommand request, CancellationToken cancellationToken) =>
    await _projectMemberRepository
        .UserHasAccess(request.ProjectId,
            _userIdentifierProvider.UserId,
            request.Permission);
}