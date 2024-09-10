using PhoenixTask.Domain.Authorities;

namespace PhoenixTask.Application.Authentication;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(Guid memberId);
}