using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixTask.Persistance.Repositories;

internal class WorkSpaceMemberRepository : IWorkSpaceMemberRepository
{
    public Task<bool> UserHasRoleAsync(User user, WorkSpace workSpace, Role role)
    {
        throw new NotImplementedException();
    }
}
