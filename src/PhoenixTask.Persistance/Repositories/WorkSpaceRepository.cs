using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Persistance.Repositories;

internal sealed class WorkSpaceRepository(IDbContext dbContext) : GenericRepository<WorkSpace>(dbContext), IWorkSpaceRepository
{
    public Task<IEnumerable<WorkSpace>> GetAll(Guid userId)
    {
        throw new NotImplementedException();
    }
}
