using PhoenixTask.Domain.Abstractions.Maybe;

namespace PhoenixTask.Domain.Projects;

public interface IBoardRepository
{
    public void Insert(Board board);
    public Task<IEnumerable<Board>> GetAllBoardsByProjectIdAsync(Guid projectId);
    public Task<Maybe<Board>> GetBoardByIdAsync(Guid boardId);
    public void Update(Board board);
    public void Remove(Board board);
}
