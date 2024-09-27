using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Tasks;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;
using TaskEntity = PhoenixTask.Domain.Tasks.Task;

namespace PhoenixTask.Application.Tasks.CreateTask;

internal sealed class CreateTaskCommandHandler
    (IUserIdentifierProvider userIdentifierProvider,
    IUserRepository userRepository,
    IBoardRepository boardRepository,
    ITaskRepository taskRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateTaskCommand, Result<string>>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IBoardRepository _boardRepository = boardRepository;
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<string>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var nameResult = Name.Create(request.Name);

        if (nameResult.IsFailure)
        {
            return Result.Failure<string>(nameResult.Error);
        }

        var maybeUser =await _userRepository.GetByIdAsync(_userIdentifierProvider.UserId);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure<string>(DomainErrors.User.NotFound);
        }

        var user = maybeUser.Value;

        var maybeBoard = await _boardRepository.GetByIdAsync(request.BoardId);

        if (maybeBoard.HasNoValue)
        {
            return Result.Failure<string>(DomainErrors.Board.NotFound);
        }

        var board = maybeBoard.Value;

        // check if user can create task in board

        var task = TaskEntity.Create(nameResult.Value, user, board, request.Description, request.DeadLine, request.Priority, request.Order);

        _taskRepository.Insert(task);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return task.Id.ToString();
    }
}
