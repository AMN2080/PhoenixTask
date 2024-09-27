using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Tasks;
using TaskEntity = PhoenixTask.Domain.Tasks.Task;

namespace PhoenixTask.Persistance.Repositories;

internal sealed class TaskRepository(IDbContext dbContext) : GenericRepository<TaskEntity>(dbContext), ITaskRepository
{
    public async Task<IEnumerable<TaskEntity>> GetByBoard(Guid boardId) 
        => await DbContext.Set<TaskEntity>()
            .AsNoTracking()
            .Where(x => x.BoardId == boardId)
            .ToListAsync();
}
