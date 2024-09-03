using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Boards;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Boards.GetBoard;

internal sealed class GetBoardQueryHandler(
    IBoardRepository boardRepository,
    IProjectRepository projectRepository,
    IWorkSpaceRepository workSpaceRepository,
    IUserIdentifierProvider userIdentifierProvider) : IQueryHandler<GetBoardQuery, Maybe<BoardResult>>
{
    private readonly IBoardRepository _boardRepository = boardRepository;
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    public async Task<Maybe<BoardResult>> Handle(GetBoardQuery request, CancellationToken cancellationToken)
    {
        var maybeBoard = await _boardRepository.GetByIdAsync(request.BoardId);

        if (maybeBoard.HasNoValue)
        {
            return Maybe<BoardResult>.None;
        }

        var board = maybeBoard.Value;

        #region UserCanGetBoard 

        var maybeProject = await _projectRepository.GetByIdAsync(board.ProjectId);

        var maybeWorkspace = await _workSpaceRepository.GetByIdAsync(maybeProject.Value.WorkSpaceId);

        if (maybeWorkspace.Value.OwnerId != _userIdentifierProvider.UserId)
        {
            return Maybe<BoardResult>.None;
        }

        #endregion

        return new BoardResult(board.Id, board.Name, board.Order, board.Color);
    }
}
