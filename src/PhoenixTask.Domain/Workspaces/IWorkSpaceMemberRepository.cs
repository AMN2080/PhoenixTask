using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Domain.Workspaces;

public interface IWorkSpaceMemberRepository
{
    Task<bool> UserHasRoleAsync(User user,WorkSpace workSpace, Role role);
    void Insert(WorkSpaceMember workSpaceMember);
    Task<Maybe<WorkSpaceMember>> GetMemberByIdAsync(Guid workSpaceId,Guid memberId);
    Task<IEnumerable<WorkSpaceMember>> GetMembersByIdAsync(Guid workSpaceId);
    void Remove(WorkSpaceMember workSpaceMember);
}
