using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Boards;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Boards.GetBoardsByProject;

internal sealed class GetBoardByProjectQueryHandler(
    IBoardRepository boardRepository,
    IProjectRepository projectRepository,
    IWorkSpaceRepository workSpaceRepository,
    IUserIdentifierProvider userIdentifierProvider) : IQueryHandler<GetBoardByProjectQuery, IEnumerable<BoardResult>>
{
    private readonly IBoardRepository _boardRepository = boardRepository;
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    public async Task<IEnumerable<BoardResult>> Handle(GetBoardByProjectQuery request, CancellationToken cancellationToken)
    {
        var maybeProject = await _projectRepository.GetByIdAsync(request.ProjectId);

        if (maybeProject.HasNoValue)
        {
            return [];
        }

        var project = maybeProject.Value;

        #region UserCanAccessBoards

        var maybeWorkspace = await _workSpaceRepository.GetByIdAsync(project.WorkSpaceId);

        if (maybeWorkspace.Value.OwnerId != _userIdentifierProvider.UserId)
        {
            return [];
        }

        #endregion

        var boards = await _boardRepository.GetAllBoardsByProjectIdAsync(request.ProjectId);

        return boards.Select(e => new BoardResult(e.Id, e.Name, e.Order, e.Color));
    }
}
