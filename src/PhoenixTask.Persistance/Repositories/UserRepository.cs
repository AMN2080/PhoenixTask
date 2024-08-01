using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Persistance.Repositories;

internal sealed class UserRepository(IDbContext dbContext) : GenericRepository<User>(dbContext), IUserRepository
{
    public async Task<Maybe<User>> GetByEmailAsync(Email email)
        => await DbContext.Set<User>().FirstOrDefaultAsync(e => e.Email.Value == email.Value);

    public async Task<Maybe<User>> GetByIdAsync(Guid userId) 
        => await DbContext.Set<User>().FirstOrDefaultAsync(e => e.Id == userId);

    public async Task<Maybe<User>> GetByUsernameAsync(UserName userName)
        => await DbContext.Set<User>().FirstOrDefaultAsync(e => e.UserName.Value == userName.Value);

    public async Task<bool> IsEmailUniqueAsync(Email email)
        => !await DbContext.Set<User>().AnyAsync(e => e.Email.Value == email.Value);

    public async Task<bool> IsUsernameUniqueAsync(UserName userName)
        => !await DbContext.Set<User>().AnyAsync(e => e.UserName.Value == userName.Value);
}