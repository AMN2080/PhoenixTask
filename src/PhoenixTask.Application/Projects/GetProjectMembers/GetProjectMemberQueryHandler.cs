using MediatR;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.WorkSpaces.CheckPermission;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Projects;
using ProjectMemberModel = PhoenixTask.Contracts.Projects.ProjectMember;

namespace PhoenixTask.Application.Projects.GetProjectMembers;

internal sealed class GetProjectMemberQueryHandler(
    ISender sender,
    IProjectMemberRepository projectMemberRepository
    ) : IQueryHandler<GetProjectMemberQuery, Maybe<IEnumerable<ProjectMemberModel>>>
{
    private readonly ISender _sender = sender;
    private readonly IProjectMemberRepository _projectMemberRepository = projectMemberRepository;

    public async Task<Maybe<IEnumerable<ProjectMemberModel>>> Handle(GetProjectMemberQuery request, CancellationToken cancellationToken)
    {
        var hasAccess = await _sender.Send(new HasWorkSpacePermissionCommand(request.ProjectId, Domain.Authorities.PermissionType.ManageUsers));

        if (!hasAccess)
        {
            return Maybe<IEnumerable<ProjectMemberModel>>.None;
        }

        var users = await _projectMemberRepository.GetWorkSpaceUsers(request.ProjectId);

        if (users.Any())
        {
            return Maybe<IEnumerable<ProjectMemberModel>>.From(users.Select(x => new ProjectMemberModel(x.Id, x.Email)));
        }

        return Maybe<IEnumerable<ProjectMemberModel>>.From([]);
    }
}
