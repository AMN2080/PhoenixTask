using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Tasks.CreateTask;

public sealed record CreateTaskCommand(string Name, Guid BoardId, string Description, DateTime DeadLine, int Order, int Priority) : ICommand<Result<string>>;
