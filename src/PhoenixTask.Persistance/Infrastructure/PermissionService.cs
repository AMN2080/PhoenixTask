using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Authentication;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Persistance.Infrastructure;

internal sealed class PermissionService(IDbContext context) : IPermissionService
{
    private readonly IDbContext _context = context;
    public async Task<HashSet<string>> GetPermissionsAsync(Guid memberId)
    {
        var roles = await _context.Set<WorkSpaceMember>()
             .Include(e => e.Roles)
             .ThenInclude(e => e.Permissions)
             .AsNoTracking()
             .Where(e => e.UserId == memberId)
             .Select(x => x.Roles)
             .ToArrayAsync();

        return roles
            .SelectMany(x => x)
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
