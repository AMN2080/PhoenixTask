using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Projects;

namespace PhoenixTask.Persistance.Repositories;

internal sealed class ProjectRepository(IDbContext dbContext) : GenericRepository<Project>(dbContext), IProjectRepository
{
    public async Task<IEnumerable<Project>> GetAllProjectsByWorkSpaceIdAsync(Guid workSpaceId) 
        => await DbContext.Set<Project>().Where(p => p.WorkSpaceId == workSpaceId).ToListAsync();
}