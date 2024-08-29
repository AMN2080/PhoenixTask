using PhoenixTask.Domain.Abstractions.Maybe;

namespace PhoenixTask.Domain.Projects;

public interface IProjectRepository
{
    public void Insert(Project project);
    public Task<IEnumerable<Project>> GetAllProjectsByWorkSpaceIdAsync(Guid workSpaceId);
    public Task<Maybe<Project>> GetProjectByIdAsync(Guid projectId);
    public void Update(Project project);
    public void Remove(Project project);
}
