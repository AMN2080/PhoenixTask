using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.WorkSpaces;
using PhoenixTask.Domain.Abstractions.Maybe;

namespace PhoenixTask.Application.WorkSpaces.GetAllWorkSpaces;

public sealed record GetAllWorkspacesQuery :IQuery<Maybe<IEnumerable<WorkSpaceResult>>>;
