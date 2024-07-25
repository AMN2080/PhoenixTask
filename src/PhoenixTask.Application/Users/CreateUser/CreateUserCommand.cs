using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Users.CreateUser;

public sealed record CreateUserCommand(string UserName,string Email,string Password) : ICommand<Result<Guid>>;