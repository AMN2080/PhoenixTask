using PhoenixTask.Application.Abstractions.Common;

namespace PhoenixTask.Infrastructure.Common;

internal sealed class MachineDateTime : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}
