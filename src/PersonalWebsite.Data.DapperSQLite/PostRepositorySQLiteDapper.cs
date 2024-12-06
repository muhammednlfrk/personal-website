using Dapper;
using PersonalWebsite.Data.DapperSQLite.Helpers;
using PersonalWebsite.Data.Entities;
using System.Data;
using System.Data.SQLite;

namespace PersonalWebsite.Data.DapperSQLite;

public sealed class PostRepositorySQLiteDapper : IPostRepository
{
    private readonly SQLiteConnectionProvider _connectionProvider;

    public PostRepositorySQLiteDapper(SQLiteConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    #region IPostRepository Implementation

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        using SQLiteConnection conn = _connectionProvider.GetPostDbConnection();
        return await conn.QueryAsync<Post>(
           sql: "SELECT PostId, PostTitle, PostDescription, CreationTime, UpdateTime FROM Posts ORDER BY CreationTime;");
    }

    public async Task<Post?> GetAsync(string postId)
    {
        using SQLiteConnection conn = _connectionProvider.GetPostDbConnection();
        return await conn.QueryFirstOrDefaultAsync<Post?>(
            sql: "SELECT PostId, PostTitle, PostDescription, CreationTime, UpdateTime FROM Posts WHERE PostId=@postId;",
            param: new { postId });
    }

    public async Task<IEnumerable<Post>> GetPagedAsync(uint page, uint pageSize)
    {
        using SQLiteConnection conn = _connectionProvider.GetPostDbConnection();
        return await conn.QueryAsync<Post>(
           sql: "SELECT PostId, PostTitle, PostDescription, CreationTime, UpdateTime FROM Posts ORDER BY CreationTime LIMIT @PageSize OFFSET @PageSize * (@PageNumber - 1);",
           param: new
           {
               PageSize = pageSize,
               PageNumber = page
           });
    }

    public async Task<Post?> CreateAsync(Post entity)
    {
        using SQLiteConnection conn = _connectionProvider.GetPostDbConnection();

        string? existingPostId = await conn.QueryFirstOrDefaultAsync<string?>(
            sql: "SELECT PostId FROM Posts WHERE PostId=@PostId;",
            param: new { entity.PostId },
            commandType: CommandType.Text);
        if (!string.IsNullOrEmpty(existingPostId))
            return null;

        Post newPost = new()
        {
            PostId = entity.PostId.Trim().ToLower(),
            PostTitle = entity.PostTitle.Trim(),
            PostDescription = entity.PostDescription.Trim(),
            CreationTime = DateTime.Now,
            UpdateTime = null,
        };

        int affectedRowCount = await conn.ExecuteAsync(
            sql: "INSERT INTO Posts(PostId, PostTitle, PostDescription, CreationTime, UpdateTime) VALUES(@PostId, @PostTitle, @PostDescription, @CreationTime, @UpdateTime);",
            param: newPost,
            commandType: CommandType.Text);

        // There is almost nothing changed when post added to the db.
        return entity;
    }

    public async Task<bool> DeleteAsync(string postId)
    {
        if (string.IsNullOrEmpty(postId)) return false;
        using SQLiteConnection conn = _connectionProvider.GetPostDbConnection();
        int affectedRowCount = await conn.ExecuteAsync(
            sql: "DELETE FROM Posts WHERE PostId=@postId;",
            param: new { postId },
            commandType: CommandType.Text);
        return affectedRowCount > 0;
    }

    public async Task<bool> DeleteAsync(Post entity)
    {
        return await DeleteAsync(postId: entity.PostId);
    }

    public async Task<Post?> UpdateAsync(Post entity)
    {
        using SQLiteConnection connection = _connectionProvider.GetPostDbConnection();

        Post? targetPost = await connection.QueryFirstOrDefaultAsync<Post?>(
            sql: "SELECT PostId, PostTitle, PostDescription, CreationTime, UpdateTime FROM Posts WHERE PostId=@PostId;",
            param: new { entity.PostId },
            commandType: CommandType.Text);
        if (string.IsNullOrEmpty(targetPost?.PostId))
            return null;

        targetPost.PostId = entity.PostId.Trim().ToLower();
        targetPost.PostDescription = entity.PostDescription.Trim();
        targetPost.PostTitle = entity.PostTitle.Trim();
        targetPost.UpdateTime = DateTime.Now;
        int affectedRowCount = await connection.ExecuteAsync(
            sql: "UPDATE Posts SET PostTitle=@PostTitle, PostDescription=@PostDescription, UpdateTime=@UpdateTime WHERE PostId=@PostId;",
            param: targetPost,
            commandType: CommandType.Text);
        return affectedRowCount > 0 ? targetPost : null;
    }

    public async Task<IEnumerable<Post>> GetLast10ArticesAsync()
    {
        using SQLiteConnection conn = _connectionProvider.GetPostDbConnection();
        return await conn.QueryAsync<Post>(
           sql: "SELECT PostId, PostTitle, PostDescription, CreationTime, UpdateTime FROM Posts ORDER BY CreationTime LIMIT 10;");
    }

    #endregion
}
