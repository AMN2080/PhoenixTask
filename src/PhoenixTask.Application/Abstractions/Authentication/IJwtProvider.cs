using PhoenixTask.Domain.Users;

namespace PhoenixTask.Application.Abstractions.Authentication;
public interface IJwtProvider
{
    string Create(User user);
}
