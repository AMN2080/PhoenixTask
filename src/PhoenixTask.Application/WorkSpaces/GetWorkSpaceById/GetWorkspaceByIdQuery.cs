using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.WorkSpaces;
using PhoenixTask.Domain.Abstractions.Maybe;

namespace PhoenixTask.Application.WorkSpaces.GetWorkSpaceById;

public sealed record WorkspaceByIdQuery(Guid WorkSpaceId) : IQuery<Maybe<WorkSpaceResult>>;
