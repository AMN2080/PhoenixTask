using Microsoft.IdentityModel.Tokens;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Tasks;
using PhoenixTask.Domain.Tasks;

namespace PhoenixTask.Application.Tasks.GetTasks.GetTasksByBoard;

internal sealed class GetTasksByBoardQueryHandler
    (ITaskRepository taskRepository,
    IUserIdentifierProvider userIdentifierProvider
    ) : IQueryHandler<GetTasksByBoardQuery, IEnumerable<TaskResponse>>
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;

    public async Task<IEnumerable<TaskResponse>> Handle(GetTasksByBoardQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetByBoard(request.BoardId);

        if (tasks.IsNullOrEmpty())
        {
            return [];
        }

        return tasks
            .Where(x => x.CreatorId == _userIdentifierProvider.UserId)
            .Select(x => new TaskResponse(x.Id, x.Name, x.Description, x.DeadLine, x.Order, x.Priority));
    }
}