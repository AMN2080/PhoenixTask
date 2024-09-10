using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Projects;

namespace PhoenixTask.Application.Authentication.CheckPermission;

internal sealed class HasProjectPermissionCommandHandler(
    IDbContext dbContext,
    IUserIdentifierProvider userIdentifierProvider
    ) : ICommandHandler<HasProjectPermissionCommand, bool>
{
    private readonly IDbContext _dbContext = dbContext;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;

    public async Task<bool> Handle(HasProjectPermissionCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userIdentifierProvider.UserId;
        var userPermissions = await _dbContext.Set<ProjectMember>()
            .Include(x => x.Roles)
            .ThenInclude(x => x.Permissions)
            .Where(x => x.ProjectId == request.ProjectId && x.UserId == userId)
            .Select(x => x.Roles)
            .SelectMany(x => x)
            .SelectMany(x => x.Permissions)
            .Select(x => x.ToString())
            .ToListAsync();

        if (userPermissions?.Contains(request.Permission.ToString()) ?? false)
        {
            return true;
        }

        return false;
    }
}