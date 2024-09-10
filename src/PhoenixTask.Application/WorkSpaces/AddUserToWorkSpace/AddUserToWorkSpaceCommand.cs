using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.WorkSpaces.AddUserToWorkSpace;

public sealed record AddUserToWorkSpaceCommand(Guid WorkSpaceId,Guid UserId,int RoleId) : ICommand<Result>;
internal sealed class AddUserToWorkSpaceCommandHandler(
    IWorkSpaceMemberRepository workSpaceMemberRepository) : ICommandHandler<AddUserToWorkSpaceCommand, Result>
{
    private readonly IWorkSpaceMemberRepository _workSpaceMemberRepository = workSpaceMemberRepository;
    public Task<Result> Handle(AddUserToWorkSpaceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
