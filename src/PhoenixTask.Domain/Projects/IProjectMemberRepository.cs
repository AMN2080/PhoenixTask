using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Domain.Projects;

public interface IProjectMemberRepository
{
    void Insert(ProjectMember projectMember);
    Task<Maybe<ProjectMember>> GetMemberByIdAsync(Guid projectId, Guid userId);
    Task<IEnumerable<ProjectMember>> GetMembersByIdAsync(Guid projectId);
    void Remove(ProjectMember projectMember);
    Task<bool> UserHasAccess(Guid projectId, Guid userId, PermissionType permissionType);
    Task<bool> AnyAsync(Guid projectId, Guid userId, int roleValue);

    Task<IEnumerable<User>> GetWorkSpaceUsers(Guid projectId);
}
