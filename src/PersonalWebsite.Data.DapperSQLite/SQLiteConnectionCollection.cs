namespace PersonalWebsite.Data.DapperSQLite;

public sealed class SQLiteConnectionCollection : IDisposable
{
    // key: connection string name
    // value: connection string
    private readonly Dictionary<string, string> _connectionStrings;

    public SQLiteConnectionCollection() => _connectionStrings = [];

    public bool Add(string name, string conStr)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(conStr);
        return _connectionStrings.TryAdd(name, conStr);
    }

    public void Dispose() => _connectionStrings.Clear();

    public SQLiteConnectionProvider BuildProvider()
    {
        return new SQLiteConnectionProvider(_connectionStrings);
    }
}
