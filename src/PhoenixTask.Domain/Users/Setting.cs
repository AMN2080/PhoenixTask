using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Domain.Users;

public sealed class Setting : Entity
{
    public Key Key { get; set; } = Key.Default;
    public string Value { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}