using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Projects;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Projects.GetProjectById;

public sealed record GetProjectByIdQuery(Guid ProjectId) : IQuery<Result<ProjectResult>>;
