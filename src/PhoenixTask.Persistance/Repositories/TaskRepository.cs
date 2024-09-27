using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Tasks;
using TaskEntity = PhoenixTask.Domain.Tasks.Task;

namespace PhoenixTask.Persistance.Repositories;

internal sealed class TaskRepository(IDbContext dbContext) : GenericRepository<TaskEntity>(dbContext), ITaskRepository
{
}
