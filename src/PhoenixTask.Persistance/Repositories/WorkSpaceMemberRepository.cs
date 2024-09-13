using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Persistance.Repositories;

internal class WorkSpaceMemberRepository(IDbContext dbContext) : GenericRepository<WorkSpaceMember>(dbContext), IWorkSpaceMemberRepository
{
    public async Task<Maybe<WorkSpaceMember>> GetMemberByIdAsync(Guid workSpaceId, Guid memberId)
        => Maybe<WorkSpaceMember>.From(await DbContext.Set<WorkSpaceMember>()
            .SingleOrDefaultAsync(x => x.UserId == memberId && x.WorkSpaceId == workSpaceId));

    public async Task<IEnumerable<WorkSpaceMember>> GetMembersByIdAsync(Guid workSpaceId)
        => DbContext.Set<WorkSpaceMember>().Where(x => x.WorkSpaceId == workSpaceId);
    public async Task<bool> UserHasRoleAsync(User user, WorkSpace workSpace, Role role)
    {
        return await DbContext.Set<WorkSpaceMember>()
            .Include(x => x.Roles)
            .Where(x => x.UserId == user.Id && x.WorkSpaceId == workSpace.Id)
            .SelectMany(x => x.Roles)
            .AnyAsync(x => x == role);
    }
}