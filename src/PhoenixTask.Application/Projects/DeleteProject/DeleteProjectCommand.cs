using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Projects.DeleteProject;

public sealed record DeleteProjectCommand(Guid ProjectId) : ICommand<Result>;
