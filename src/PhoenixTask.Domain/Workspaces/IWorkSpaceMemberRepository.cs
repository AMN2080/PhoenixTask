using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Domain.Workspaces;

public interface IWorkSpaceMemberRepository
{
    Task<bool> UserHasRoleAsync(User user,WorkSpace workSpace, Role role);
}
