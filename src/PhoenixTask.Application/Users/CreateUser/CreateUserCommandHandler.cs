using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<Guid>>
{
    public Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
