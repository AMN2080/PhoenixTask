using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Tasks;

namespace PhoenixTask.Application.Tasks.GetTasks.GetTasksByBoard;

public sealed record GetTasksByBoardQuery(Guid BoardId) : IQuery<IEnumerable<TaskResponse>>;
