using PhoenixTask.Domain.Abstractions.Maybe;

namespace PhoenixTask.Domain.Tasks;

public interface ITaskRepository
{
    public void Insert(Task task);
    public void Update(Task task);
    public void Remove(Task task);
    public Task<Maybe<Task>> GetByIdAsync(Guid id);
}
