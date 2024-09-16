using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Persistance.Repositories;

internal class WorkSpaceMemberRepository(IDbContext dbContext) : GenericRepository<WorkSpaceMember>(dbContext), IWorkSpaceMemberRepository
{
    public Task<bool> AnyAsync(Guid workSpaceId, Guid userId, int roleValue)
    {
        throw new NotImplementedException();
    }

    public async Task<Maybe<WorkSpaceMember>> GetMemberByIdAsync(Guid workSpaceId, Guid memberId)
        => Maybe<WorkSpaceMember>.From(await DbContext.Set<WorkSpaceMember>()
            .SingleOrDefaultAsync(x => x.UserId == memberId && x.WorkSpaceId == workSpaceId));

    public async Task<IEnumerable<WorkSpaceMember>> GetMembersByIdAsync(Guid workSpaceId)
        => DbContext.Set<WorkSpaceMember>().Where(x => x.WorkSpaceId == workSpaceId);

    public Task<IEnumerable<User>> GetWorkSpaceUsers(Guid workSpaceId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UserHasAccess(Guid workSpaceId, Guid userId, PermissionType permissionType)
    {
        string query = """
            SELECT 1
            FROM User u
            JOIN MemberRole m ON u.Id = m.UserId
            JOIN RolePermission r ON m.RoleValue = r.RoleValue
            WHERE u.UserId = @UserId 
            AND r.PermissionId = @PermissionId
            AND m.WorkSpaceId = @WorkSpaceId
            """;

        SqlParameter[] parameters =
        {
            new SqlParameter("@UserId", userId),
            new SqlParameter("@WorkSpaceId", workSpaceId)
        };

        var result = await DbContext.ExecuteSqlAsync(query, parameters);

        return result > 0;
    }
}