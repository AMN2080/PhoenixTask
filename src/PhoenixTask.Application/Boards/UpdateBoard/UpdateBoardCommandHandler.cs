using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Boards.UpdateBoard;

internal sealed class UpdateBoardCommandHandler(
     IUserIdentifierProvider userIdentifierProvider,
    IProjectRepository projectRepository,
    IWorkSpaceRepository workSpaceRepository,
    IBoardRepository boardRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateBoardCommand, Result>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IBoardRepository _boardRepository = boardRepository;
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        var nameResult = Name.Create(request.Name);

        if (nameResult.IsFailure)
        {
            return Result.Failure(nameResult.Error);
        }

        var maybeBoard = await _boardRepository.GetByIdAsync(request.BoardId);

        if (maybeBoard.HasNoValue)
        {
            return Result.Failure(DomainErrors.Board.NotFound);
        }

        var board = maybeBoard.Value;

        #region UserCanUpdateBoardValidation

        var maybeProject = await _projectRepository.GetByIdAsync(board.ProjectId);

        var maybeWorkspace = await _workSpaceRepository.GetByIdAsync(maybeProject.Value.WorkSpaceId);

        if (maybeWorkspace.Value.OwnerId != _userIdentifierProvider.UserId)
        {
            return Result.Failure<string>(DomainErrors.User.InvalidPermissions);
        }

        #endregion

        board.Update(nameResult.Value, request.Order, request.Color);

        _boardRepository.Update(board);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
