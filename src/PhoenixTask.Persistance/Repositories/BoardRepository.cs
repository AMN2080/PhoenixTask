using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Projects;

namespace PhoenixTask.Persistance.Repositories;

internal sealed class BoardRepository(IDbContext dbContext) : GenericRepository<Board>(dbContext), IBoardRepository
{
    public async Task<IEnumerable<Board>> GetAllBoardsByProjectIdAsync(Guid projectId) 
        => await DbContext.Set<Board>().Where(b => b.ProjectId == projectId).ToListAsync();
}
