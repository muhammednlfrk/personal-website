using System.Data.SQLite;

namespace PersonalWebsite.Data.DapperSQLite;

public sealed class SQLiteConnectionProvider
{
    // key: connection string name
    // value: connection string
    private readonly IReadOnlyDictionary<string, string> _connectionStrings;

    public SQLiteConnectionProvider(IReadOnlyDictionary<string, string> connectionStrings)
    {
        ArgumentNullException.ThrowIfNull(connectionStrings, nameof(connectionStrings));
        _connectionStrings = connectionStrings;
    }

    public SQLiteConnection GetConnection(string name)
    {
        if (_connectionStrings.TryGetValue(name, out string? connectionString))
            return new SQLiteConnection(connectionString);

        throw new ArgumentException("Invalid connection string name.");
    }
}
