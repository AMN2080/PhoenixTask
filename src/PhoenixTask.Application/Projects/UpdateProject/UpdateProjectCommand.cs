using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Projects.UpdateProject;

public sealed record UpdateProjectCommand(Guid ProjectId, string Name) : ICommand<Result>;
