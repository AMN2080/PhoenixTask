using Microsoft.AspNetCore.Authorization;

namespace PhoenixTask.WebApi.Infrastructure;

public class PermissionRequierment(string permission)
    : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}