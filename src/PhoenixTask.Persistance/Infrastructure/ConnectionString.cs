namespace PhoenixTask.Persistance.Infrastructure;

public sealed class ConnectionString
{
    public const string SettingsKey = "PhoenixTaskDb";
    public bool InMemoryDb { get; }
    public ConnectionString(string value, bool inmemory = false) => (Value,InMemoryDb) = (value,inmemory);

    public string Value { get; }

    public static implicit operator string(ConnectionString connectionString) => connectionString.Value;
}
