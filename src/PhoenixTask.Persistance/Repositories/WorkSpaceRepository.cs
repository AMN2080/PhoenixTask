using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Persistance.Repositories;

internal sealed class WorkSpaceRepository(IDbContext dbContext) : GenericRepository<WorkSpace>(dbContext), IWorkSpaceRepository
{
    public Task<IEnumerable<WorkSpace>> GetAll(Guid userId)
    {
        var sharedWorkSpaces = (from member in DbContext.Set<WorkSpaceMember>().Where(x => x.UserId == userId).AsNoTracking()
                                join workspace in DbContext.Set<WorkSpace>().AsNoTracking() on member.WorkSpaceId equals workspace.Id
                                select workspace);

        var userWorkSpaces = DbContext.Set<WorkSpace>().Where(x => x.OwnerId == userId).AsNoTracking();
        
        return Task.FromResult(sharedWorkSpaces.Union(userWorkSpaces).AsEnumerable());
    }
}