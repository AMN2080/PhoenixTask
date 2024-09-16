using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.WorkSpaces;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.WorkSpaces.GetAllWorkSpaces;

internal sealed class GetAllWorkspacesQueryHandler
    (IUserIdentifierProvider userIdentifierProvider,
    IWorkSpaceRepository workSpaceRepository,
    IDbContext dbContext): IQueryHandler<GetAllWorkspacesQuery, Maybe<IEnumerable<WorkSpaceResult>>>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IDbContext _dbContext = dbContext;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    public async Task<Maybe<IEnumerable<WorkSpaceResult>>> Handle(GetAllWorkspacesQuery request, CancellationToken cancellationToken)
    {
        var userId = _userIdentifierProvider.UserId;

        var result = await _workSpaceRepository.GetAll(userId);

        return Maybe<IEnumerable<WorkSpaceResult>>.From(result.Select(x => new WorkSpaceResult(x.Id, x.Name, x.Color)));
    }
}
