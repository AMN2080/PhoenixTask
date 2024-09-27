using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Tasks.DeleteTask;

public sealed record DeleteTaskCommand(Guid TaskId) : ICommand<Result>;
