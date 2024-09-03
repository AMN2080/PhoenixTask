using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.Boards.CreateBoard;

internal sealed class CreateBoardCommandHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IProjectRepository projectRepository,
    IWorkSpaceRepository workSpaceRepository,
    IBoardRepository boardRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateBoardCommand, Result<string>>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IBoardRepository _boardRepository = boardRepository;
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<string>> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var nameResult = Name.Create(request.Name);

        if(nameResult.IsFailure)
        {
            return Result.Failure<string>(nameResult.Error);
        }

        var maybeProject = await _projectRepository.GetByIdAsync(request.ProjectId);

        if(maybeProject.HasNoValue)
        {
            return Result.Failure<string>(DomainErrors.Project.NotFound);
        }

        var project = maybeProject.Value;

        #region UserCanAddBoardValidation

        var maybeWorkspace =await _workSpaceRepository.GetByIdAsync(project.WorkSpaceId);

        if(maybeWorkspace.Value.OwnerId != _userIdentifierProvider.UserId)
        {
            return Result.Failure<string>(DomainErrors.User.InvalidPermissions);
        }

        #endregion

        var board = Board.Create(project, nameResult.Value, request.Order, request.Color);

        _boardRepository.Insert(board);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return board.Id.ToString();
    }
}
