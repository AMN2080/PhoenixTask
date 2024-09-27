using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Tasks;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Tasks.UpdateTask;

internal sealed class UpdateTaskCommandHandler
    (ITaskRepository taskRepository,
    IUserIdentifierProvider userIdentifierProvider,
    IBoardRepository boardRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateTaskCommand, Result>
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IBoardRepository _boardRepository = boardRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var nameResult = Name.Create(request.Name);

        if (nameResult.IsFailure)
        {
            return Result.Failure<string>(nameResult.Error);
        }

        var maybeBoard = await _boardRepository.GetByIdAsync(request.BoardId);

        if (maybeBoard.HasNoValue)
        {
            return Result.Failure<string>(DomainErrors.Board.NotFound);
        }

        var board = maybeBoard.Value;

        var maybeTask = await _taskRepository.GetByIdAsync(request.TaskId);

        if (maybeTask.HasNoValue)
        {
            return Result.Failure(DomainErrors.Task.NotFound);
        }

        var task = maybeTask.Value;

        if (task.CreatorId != _userIdentifierProvider.UserId)
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        task.Update(nameResult.Value, board, request.Description, request.DeadLine, request.Priority, request.Order);

        _taskRepository.Update(task);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
