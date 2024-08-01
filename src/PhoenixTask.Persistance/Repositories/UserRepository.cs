using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Persistance.Repositories;

internal sealed class UserRepository(IDbContext dbContext) : GenericRepository<User>(dbContext), IUserRepository
{
    public async Task<bool> IsEmailUniqueAsync(Email email) 
        => await DbContext.Set<User>().AnyAsync(e => e.Email == email.Value);

    public async Task<bool> IsUsernameUniqueAsync(UserName userName)
        => await DbContext.Set<User>().AnyAsync(e => e.UserName == userName.Value);
}