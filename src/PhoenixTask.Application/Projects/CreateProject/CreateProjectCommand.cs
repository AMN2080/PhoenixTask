using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Projects.CreateProject;

public sealed record CreateProjectCommand(string Name, Guid WorkSpaceId) : ICommand<Result<string>>;
