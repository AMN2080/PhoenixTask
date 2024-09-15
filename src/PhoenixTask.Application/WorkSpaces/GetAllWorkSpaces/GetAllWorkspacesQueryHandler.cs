using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.WorkSpaces;
using PhoenixTask.Domain.Abstractions.Maybe;
using Member = PhoenixTask.Domain.Workspaces.WorkSpaceMember;
using WorkSpaceEntity = PhoenixTask.Domain.Workspaces.WorkSpace;

namespace PhoenixTask.Application.WorkSpaces.GetAllWorkSpaces;

internal sealed class GetAllWorkspacesQueryHandler
    (IUserIdentifierProvider userIdentifierProvider,
    IDbContext dbContext): IQueryHandler<GetAllWorkspacesQuery, Maybe<IEnumerable<WorkSpaceResult>>>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IDbContext _dbContext = dbContext; 
    public async Task<Maybe<IEnumerable<WorkSpaceResult>>> Handle(GetAllWorkspacesQuery request, CancellationToken cancellationToken)
    {
        var userId = _userIdentifierProvider.UserId;

        var workspacesResult =await _dbContext.Set<WorkSpaceEntity>()
            .AsNoTracking()
            .Where(w => w.OwnerId == userId)
            .Select(w => new WorkSpaceResult(w.Id, w.Name.Value, w.Color))
            .ToListAsync();

        var sharedWorkSpaces = await _dbContext.Set<Member>()
            .Include(e=>e.WorkSpace)
            .AsNoTracking()
            .Where(e=>e.UserId == userId)
            .Select(w => new WorkSpaceResult(w.WorkSpace.Id, w.WorkSpace.Name.Value, w.WorkSpace.Color))
            .ToListAsync();

        return Maybe<IEnumerable<WorkSpaceResult>>.From(workspacesResult.Union(sharedWorkSpaces));
    }
}
