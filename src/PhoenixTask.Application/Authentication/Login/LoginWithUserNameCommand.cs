using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Users;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Authentication.Login;

public sealed record LoginWithUserNameCommand(string Username, string Password) : ICommand<Result<TokenResponse>>;
