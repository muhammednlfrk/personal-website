using Dapper;
using System.Data;
using System.Data.SQLite;

namespace PersonalWebsite.Data.DapperSQLite;
public static class SQLiteUserDbCreator
{
    public static void CreateUserDbIfNotExists(string connectionString)
    {
        SQLiteConnectionStringBuilder conStrBuilder = new(connectionString);
        if (!File.Exists(conStrBuilder.DataSource))
        {
            SQLiteConnection.CreateFile(conStrBuilder.DataSource);
            using SQLiteConnection connection = new(connectionString);
            createUsersTable(connection);
        }
    }

    private static void createUsersTable(SQLiteConnection connection)
    {
        connection.Execute(
                      sql: "CREATE TABLE IF NOT EXISTS Users (UserId INTEGER PRIMARY KEY AUTOINCREMENT,UserFullName NVARCHAR(128) NOT NULL,UserName NVARCHAR(64) NOT NULL UNIQUE,UserPassword NCHAR(32) NOT NULL,CreationTime TEXT NOT NULL,UpdateTime TEXT);",
                      commandTimeout: 15,
                      commandType: CommandType.Text);
    }
}
