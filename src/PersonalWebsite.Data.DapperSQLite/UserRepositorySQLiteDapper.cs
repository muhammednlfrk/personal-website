using Dapper;
using PersonalWebsite.Data.DapperSQLite.Helpers;
using PersonalWebsite.Data.Entities;
using System.Data;
using System.Data.SQLite;

namespace PersonalWebsite.Data.DapperSQLite;

public sealed class UserRepositorySQLiteDapper(SQLiteConnectionProvider connectionProvider) : IUserRepository
{
    private readonly SQLiteConnectionProvider _connectionProvider = connectionProvider;

    #region IUserRpository Implementation

    public async Task<User?> CreateAsync(User entity)
    {
        using SQLiteConnection conn = _connectionProvider.GetUserDbConnection();
        entity.CreationTime = DateTime.Now;
        entity.UserFullName = entity.UserFullName.Trim();
        entity.UserName = entity.UserName.Trim();
        await conn.ExecuteAsync(
            sql: "INSERT INTO Users(UserFullName, UserName, UserPassword, CreationTime) VALUES (@UserFullName, @UserName, @UserPassword, @CreationTime);",
            param: new { entity.UserFullName, entity.UserName, entity.UserPassword, entity.CreationTime },
            commandType: CommandType.Text);
        return await queryFirstAsync("UserName=@userName", new { userName = entity.UserName });
    }

    public async Task<User?> UpdateAsync(User entity)
    {
        using SQLiteConnection conn = _connectionProvider.GetUserDbConnection();
        User? targetEntity = await queryFirstAsync("UserId=@userId", new { userId = entity.UserId });
        if (targetEntity == null) throw new ArgumentException("invalid target.", nameof(entity.UserId));
        entity.CreationTime = targetEntity.CreationTime;
        entity.UpdateTime = DateTime.Now;
        entity.UserFullName = entity.UserFullName.Trim();
        entity.UserName = entity.UserName.Trim();
        await conn.ExecuteAsync(
            sql: "UPDATE Users SET UserFullName=@UserFullName, UserName=@UserName, UserPassword=@UserPassword, UpdateTime=@UpdateTime WHERE UserId=@UserId;",
            param: new {entity.UserId, entity.UserFullName, entity.UserName, entity.UserPassword, entity.UpdateTime },
            commandType: CommandType.Text);
        return await queryFirstAsync("UserName=@userName", new { userName = entity.UserName });
    }

    public async Task<bool> DeleteAsync(int userId)
    {
        if (userId < 0) throw new ArgumentException($"invalid {userId}");
        using SQLiteConnection conn = _connectionProvider.GetUserDbConnection();
        using SQLiteTransaction tran = conn.BeginTransaction();
        int affectedRowCount = await conn.ExecuteAsync(
            sql: "DELETE FROM Users WHERE UserId=@userId;",
            param: new { userId },
            transaction: tran);
        if (affectedRowCount != 0 && affectedRowCount != 1)
        {
            await tran.RollbackAsync();
            throw new Exception(">1 row affected!");
        }
        await tran.CommitAsync();
        return affectedRowCount == 1;
    }

    public async Task<bool> DeleteAsync(User entity)
    {
        return await DeleteAsync(entity.UserId);
    }

    public async Task<User?> FindAsync(int userId)
    {
        return await queryFirstAsync("UserId=@userId", new { userId });
    }

    public async Task<User?> FindAsync(string username)
    {
        return await queryFirstAsync("UserName=@username", new { username });
    }

    public async Task<User?> FindAsync(string username, string password)
    {
        return await queryFirstAsync("UserName=@username AND UserPassword=@password", new { username, password });
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        // Not implemented becouse of security reasons.
        throw new NotImplementedException();
    }

    #endregion

    public async Task<User?> queryFirstAsync(string where, object? param)
    {
        using SQLiteConnection conn = _connectionProvider.GetUserDbConnection();
        return await conn.QueryFirstOrDefaultAsync<User?>(
           sql: $"SELECT UserId, UserFullName, UserName, UserPassword, CreationTime, UpdateTime FROM USERS WHERE {where};",
           param: param);
    }

    public async Task<IEnumerable<User?>> queryAsync(string where, object? param)
    {
        using SQLiteConnection conn = _connectionProvider.GetUserDbConnection();
        return await conn.QueryAsync<User?>(
           sql: $"SELECT UserId, UserFullName, UserName, UserPassword, CreationTime, UpdateTime FROM USERS WHERE {where};",
           param: param);
    }
}
