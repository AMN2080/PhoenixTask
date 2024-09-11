using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Persistance.Repositories;

internal class WorkSpaceMemberRepository : IWorkSpaceMemberRepository
{
    public Task<Maybe<WorkSpaceMember>> GetMemberByIdAsync(Guid workSpaceId, Guid memberId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<WorkSpaceMember>> GetMembersByIdAsync(Guid workSpaceId)
    {
        throw new NotImplementedException();
    }

    public void Insert(WorkSpaceMember workSpaceMember)
    {
        throw new NotImplementedException();
    }

    public void Remove(WorkSpaceMember workSpaceMember)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserHasRoleAsync(User user, WorkSpace workSpace, Role role)
    {
        throw new NotImplementedException();
    }
}
