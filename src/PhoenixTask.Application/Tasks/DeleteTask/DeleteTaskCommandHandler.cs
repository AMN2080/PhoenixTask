using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Tasks;

namespace PhoenixTask.Application.Tasks.DeleteTask;

internal sealed class DeleteTaskCommandHandler
    (IUserIdentifierProvider userIdentifierProvider,
    ITaskRepository taskRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteTaskCommand, Result>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var maybeTask = await _taskRepository.GetByIdAsync(request.TaskId);

        if (maybeTask.HasNoValue)
        {
            return Result.Failure(DomainErrors.Task.NotFound);
        }
        
        var task = maybeTask.Value;

        if(task.CreatorId != _userIdentifierProvider.UserId)
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        _taskRepository.Remove(task);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
