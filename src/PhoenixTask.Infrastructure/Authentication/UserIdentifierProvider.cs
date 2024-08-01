using Microsoft.AspNetCore.Http;
using PhoenixTask.Application.Abstractions.Authentication;
using System.Security.Claims;

namespace PhoenixTask.Infrastructure.Authentication;

internal sealed class UserIdentifierProvider(IHttpContextAccessor httpContextAccessor) : IUserIdentifierProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid UserId { get; }
        = new Guid(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor)));

    public string UserName { get; }
        = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name)
            ?? throw new ArgumentException("The username email is required.", nameof(httpContextAccessor));

    public string Email { get; }
        = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email)
            ?? throw new ArgumentException("The email is required.", nameof(httpContextAccessor));
}
