using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.Data.DapperSQLite.Helpers;
using PersonalWebsite.Data.Entities;

namespace PersonalWebsite.Data.DapperSQLite.ASPNetExtensions;

public static class ASPNetExtensions
{
    public static void AddSQLiteRepositories(this IServiceCollection serviceCollection, SQLiteDbConfiguration config)
    {
        SQLiteConnectionCollection connectionCollection = new();
        connectionCollection.Add(DBDescriptors.POST_DB, config.PostDbConnectionString);
        connectionCollection.Add(DBDescriptors.USER_DB, config.UserDbConnectionString);

        SQLiteConnectionProvider connectionProvider = connectionCollection.BuildProvider();
        serviceCollection.AddSingleton(connectionProvider);

        serviceCollection.AddSingleton<IRepository<Post>, PostRepositorySQLiteDapper>();
        serviceCollection.AddSingleton<IPagedRepository<Post>, PostRepositorySQLiteDapper>();
        serviceCollection.AddSingleton<IPostRepository, PostRepositorySQLiteDapper>();

        serviceCollection.AddSingleton<IRepository<User>, UserRepositorySQLiteDapper>();
        serviceCollection.AddSingleton<IUserRepository, UserRepositorySQLiteDapper>();

        SQLitePostDbCreator.CreatePostDbIfNotExists(config.PostDbConnectionString);
        SQLiteUserDbCreator.CreateUserDbIfNotExists(config.UserDbConnectionString);
    }
}
