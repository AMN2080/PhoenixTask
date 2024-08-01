namespace PhoenixTask.Domain.Users;

public interface IUserRepository
{
    Task<bool> IsUsernameUniqueAsync(UserName userName);
    Task<bool> IsEmailUniqueAsync(Email email);
    void Insert(User user);
}
