using PhoenixTask.Domain.Abstractions.Maybe;

namespace PhoenixTask.Domain.Workspaces;

public interface IWorkSpaceRepository
{
    public void Insert(WorkSpace workSpace);
    public Task<Maybe<WorkSpace>> GetByIdAsync(Guid id);
    public void Update(WorkSpace workSpace);
    public void Remove(WorkSpace workSpace);
    public Task<IEnumerable<WorkSpace>> GetAll(Guid userId);
}
