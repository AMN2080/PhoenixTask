namespace PhoenixTask.Persistance.Infrastructure;

public sealed class ConnectionString
{
    public const string SettingsKey = "PhoenixTaskDb";

    public ConnectionString(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(ConnectionString connectionString) => connectionString.Value;
}
