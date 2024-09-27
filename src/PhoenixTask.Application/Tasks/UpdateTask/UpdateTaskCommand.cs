using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.Tasks.CreateTask;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Tasks.UpdateTask;

public sealed record UpdateTaskCommand(Guid TaskId, Guid BoardId, string Name, string Description, DateTime DeadLine, int Order, int Priority) : ICommand<Result>;
