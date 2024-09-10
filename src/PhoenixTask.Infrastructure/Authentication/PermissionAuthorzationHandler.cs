using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PhoenixTask.Application.Authentication;
using PhoenixTask.WebApi.Infrastructure;
using System.Security.Claims;

namespace PhoenixTask.Infrastructure.Authentication;

public class PermissionAuthorzationHandler(IServiceScopeFactory serviceScopeFactory)
    : AuthorizationHandler<PermissionRequierment>
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequierment requirement)
    {
        var memberId = context.User.Claims.FirstOrDefault(
            x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(memberId, out Guid parsedMemberId))
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        IPermissionService permissionService = scope.ServiceProvider
            .GetRequiredService<IPermissionService>();

        var permissions = await permissionService
            .GetPermissionsAsync(parsedMemberId);

        if(permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}
