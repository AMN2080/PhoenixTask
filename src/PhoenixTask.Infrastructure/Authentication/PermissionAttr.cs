using Microsoft.AspNetCore.Authorization;
using PhoenixTask.Domain.Authorities;

namespace PhoenixTask.Infrastructure.Authentication;

public class HasPermission(PermissionType permission) : AuthorizeAttribute(permission.ToString());
