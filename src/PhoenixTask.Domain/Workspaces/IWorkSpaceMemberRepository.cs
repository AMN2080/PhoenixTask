using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Domain.Workspaces;

public interface IWorkSpaceMemberRepository
{
    void Insert(WorkSpaceMember workSpaceMember);
    Task<Maybe<WorkSpaceMember>> GetMemberByIdAsync(Guid workSpaceId,Guid memberId);
    Task<IEnumerable<WorkSpaceMember>> GetMembersByIdAsync(Guid workSpaceId);
    void Remove(WorkSpaceMember workSpaceMember);
    Task<bool> UserHasAccess(Guid workSpaceId, Guid userId, PermissionType permissionType);
    Task<bool> AnyAsync(Guid workSpaceId, Guid userId,int roleValue);

    Task<IEnumerable<User>> GetWorkSpaceUsers(Guid workSpaceId);
}
