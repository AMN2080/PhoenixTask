using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.WorkSpaces;

namespace PhoenixTask.Application.WorkSpaces.GetAllWorkSpaces;

public sealed record GetAllWorkspacesQuery :IQuery<IEnumerable<WorkSpaceResult>>;
