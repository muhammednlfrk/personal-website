using System.Data.SQLite;

namespace PersonalWebsite.Data.DapperSQLite.Helpers;

public static class SQLiteConnectionProviderExtensions
{
    public static SQLiteConnection GetPostDbConnection(this SQLiteConnectionProvider connectionProvider)
    {
        return connectionProvider.GetConnection(DBDescriptors.POST_DB);
    }

    public static SQLiteConnection GetUserDbConnection(this SQLiteConnectionProvider connectionProvider)
    {
        return connectionProvider.GetConnection(DBDescriptors.USER_DB);
    }
}
