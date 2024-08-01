using Microsoft.AspNetCore.Http;
using PhoenixTask.Application.Abstractions.Authentication;
using System.Security.Claims;

namespace PhoenixTask.Infrastructure.Authentication;

internal sealed class UserIdentifierProvider(IHttpContextAccessor httpContextAccessor) : IUserIdentifierProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid UserId { get; }
        = new Guid(httpContextAccessor.HttpContext?.User?.FindFirstValue("userId")
            ?? throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor)));

    public string UserName { get; }
        = httpContextAccessor.HttpContext?.User?.FindFirstValue("email")
            ?? throw new ArgumentException("The user email is required.", nameof(httpContextAccessor));

    public string Email { get; }
        = httpContextAccessor.HttpContext?.User?.FindFirstValue("username")
            ?? throw new ArgumentException("The user username is required.", nameof(httpContextAccessor));
}
