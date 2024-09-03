using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Boards.DeleteBoard;

internal sealed class DeleteBoardCommandHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IProjectRepository projectRepository,
    IWorkSpaceRepository workSpaceRepository,
    IBoardRepository boardRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteBoardCommand, Result>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IBoardRepository _boardRepository = boardRepository;
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        var maybeBoard = await _boardRepository.GetByIdAsync(request.BoardId);

        if (maybeBoard.HasNoValue)
        {
            return Result.Failure(DomainErrors.Board.NotFound);
        }

        var board = maybeBoard.Value;

        #region UserCanDeleteBoardValidation

        var maybeProject = await _projectRepository.GetByIdAsync(board.ProjectId);

        var maybeWorkspace = await _workSpaceRepository.GetByIdAsync(maybeProject.Value.WorkSpaceId);

        if (maybeWorkspace.Value.OwnerId != _userIdentifierProvider.UserId)
        {
            return Result.Failure<string>(DomainErrors.User.InvalidPermissions);
        }

        #endregion

        _boardRepository.Remove(board);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
