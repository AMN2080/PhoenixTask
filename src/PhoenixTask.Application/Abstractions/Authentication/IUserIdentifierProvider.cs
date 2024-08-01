namespace PhoenixTask.Application.Abstractions.Authentication;

public interface IUserIdentifierProvider
{
    Guid UserId { get; }
    string UserName { get; }
    string Email { get; }
}