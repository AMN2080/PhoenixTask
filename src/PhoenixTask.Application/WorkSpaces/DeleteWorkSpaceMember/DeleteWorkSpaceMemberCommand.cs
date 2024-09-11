using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.WorkSpaces.DeleteWorkSpaceMember;

public sealed record DeleteWorkSpaceMemberCommand(Guid WorkSpaceId, Guid UserId) : ICommand<Result>;
