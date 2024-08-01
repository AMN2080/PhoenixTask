using PhoenixTask.Domain.Users;

namespace PhoenixTask.Application.Abstractions.Cryptography;

public interface IPasswordHasher
{
    string HashPassword(Password password);
}
