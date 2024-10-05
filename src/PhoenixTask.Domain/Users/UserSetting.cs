#pragma warning disable
using PhoenixTask.Domain.Abstractions.Guards;
using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Domain.Users;

public sealed class UserSetting : Entity
{
    private UserSetting() { }
    private UserSetting(User user, string key, string value)
        : base(Guid.NewGuid())
    {
        Ensure.NotNull(user, "The user is requierd.", nameof(user));
        Ensure.NotEmpty(key, "The key is requierd.", nameof(key));
        Ensure.NotEmpty(value, "The value is requierd.", nameof(value));

        UserId = user.Id;
        Key = key;
        Value = value;
    }
    public Guid UserId { get; private set; }
    public string Key { get; set; }
    public string Value { get; set; }
}
