using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.WorkSpaces;
using System.Collections.Immutable;
using WorkSpaceEntity = PhoenixTask.Domain.Workspaces.WorkSpace;

namespace PhoenixTask.Application.WorkSpaces.GetAllWorkSpaces;

internal sealed class GetAllWorkspacesQueryHandler
    (IUserIdentifierProvider userIdentifierProvider,
    IDbContext dbContext): IQueryHandler<GetAllWorkspacesQuery, IEnumerable<WorkSpaceResult>>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IDbContext _dbContext = dbContext; 
    public async Task<IEnumerable<WorkSpaceResult>> Handle(GetAllWorkspacesQuery request, CancellationToken cancellationToken)
    {
        var userId = _userIdentifierProvider.UserId;

        var workspacesResult = _dbContext.Set<WorkSpaceEntity>()
            .AsNoTracking()
            .Where(w => w.OwnerId == userId)
            .Select(w => new WorkSpaceResult(w.Id, w.Name.Value, w.Color))
            .ToImmutableList();

        return workspacesResult.AsReadOnly();
    }
}
