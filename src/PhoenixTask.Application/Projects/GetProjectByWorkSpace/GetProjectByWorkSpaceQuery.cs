using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Projects;

namespace PhoenixTask.Application.Projects.GetProjectByWorkSpace;

public sealed record GetProjectByWorkSpaceQuery(Guid WorkSpaceId) : IQuery<IEnumerable<ProjectResult>>;
