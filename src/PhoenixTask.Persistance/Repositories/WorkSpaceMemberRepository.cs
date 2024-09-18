using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;
using PhoenixTask.Persistance.Configurations;

namespace PhoenixTask.Persistance.Repositories;

internal class WorkSpaceMemberRepository(IDbContext dbContext
    , IUserIdentifierProvider userIdentifierProvider
    ) : GenericRepository<WorkSpaceMember>(dbContext), IWorkSpaceMemberRepository
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    public async Task<bool> AnyAsync(Guid workSpaceId, Guid userId, int roleValue)
        => await DbContext.Set<WorkSpaceMember>()
        .AnyAsync(x =>
        (x.WorkSpaceId == workSpaceId) &&
        (x.UserId == userId) &&
        (x.RoleValue == roleValue));

    public async Task<Maybe<WorkSpaceMember>> GetMemberByIdAsync(Guid workSpaceId, Guid memberId)
        => await DbContext.Set<WorkSpaceMember>()
            .SingleOrDefaultAsync(x => x.UserId == memberId && x.WorkSpaceId == workSpaceId);

    public async Task<IEnumerable<WorkSpaceMember>> GetMembersByIdAsync(Guid workSpaceId)
        => DbContext.Set<WorkSpaceMember>().Where(x => x.WorkSpaceId == workSpaceId);

    public async Task<IEnumerable<User>> GetWorkSpaceUsers(Guid workSpaceId)
    {
        var users = (from user in DbContext.Set<User>().AsNoTracking()
                     join member in DbContext.Set<WorkSpaceMember>().AsNoTracking()
                     on user.Id equals member.UserId
                     where member.WorkSpaceId == workSpaceId
                     && member.UserId != _userIdentifierProvider.UserId
                     select user).ToListAsync();
        return await users;
    }

    public async Task<bool> UserHasAccess(Guid workSpaceId, Guid userId, PermissionType permissionType)
    {
        var member = await DbContext.Set<WorkSpaceMember>()
            .AsNoTracking()
            .Where(x => x.WorkSpaceId == workSpaceId && x.UserId == userId)
            .FirstOrDefaultAsync();

        return RolePermissionConfiguration.RolePermissions.Any(x => x.RoleValue == member?.RoleValue);
    }
}