using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Domain.Users;

public interface IUserRepository
{
    Task<bool> IsUsernameUniqueAsync(UserName userName);
    Task<bool> IsEmailUniqueAsync(Email email);
    void Insert(User user);

    Task<Maybe<User>> GetByUsernameAsync(UserName userName);
    Task<Maybe<User>> GetByIdAsync(Guid userId);
    Task<Maybe<User>> GetByEmailAsync(Email email);
    void Update(User user);
}
