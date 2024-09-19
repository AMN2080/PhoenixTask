using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Users;
using PhoenixTask.Persistance.Configurations;

namespace PhoenixTask.Persistance.Repositories;

internal sealed class ProjectMemberRepository(IDbContext dbContext) : GenericRepository<ProjectMember>(dbContext), IProjectMemberRepository
{
    public async Task<bool> AnyAsync(Guid projectId, Guid userId, int roleValue)
        => await DbContext.Set<ProjectMember>()
         .AnyAsync(x =>
         (x.ProjectId == projectId) &&
         (x.UserId == userId) &&
         (x.RoleValue == roleValue));

    public async Task<Maybe<ProjectMember>> GetMemberByIdAsync(Guid projectId, Guid userId)
=> await DbContext.Set<ProjectMember>()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId);

    public async Task<IEnumerable<ProjectMember>> GetMembersByIdAsync(Guid projectId)
=> DbContext.Set<ProjectMember>().Where(x => x.ProjectId == projectId);

    public async Task<IEnumerable<User>> GetWorkSpaceUsers(Guid projectId)
    {
        var users = (from user in DbContext.Set<User>().AsNoTracking()
                     join member in DbContext.Set<ProjectMember>().AsNoTracking()
                     on user.Id equals member.UserId
                     where member.ProjectId == projectId
                     select user).ToListAsync();
        return await users;
    }

    public async Task<bool> UserHasAccess(Guid projectId, Guid userId, PermissionType permissionType)
    {
        var member = await DbContext.Set<ProjectMember>()
            .AsNoTracking()
            .Where(x => x.ProjectId == projectId && x.UserId == userId)
            .FirstOrDefaultAsync();

        return RolePermissionConfiguration.RolePermissions.Any(x => x.RoleValue == member?.RoleValue);
    }
}
