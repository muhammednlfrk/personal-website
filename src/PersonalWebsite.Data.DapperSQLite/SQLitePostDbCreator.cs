using Dapper;
using PersonalWebsite.Data.Entities;
using System.Data;
using System.Data.SQLite;

namespace PersonalWebsite.Data.DapperSQLite;

public static class SQLitePostDbCreator
{
    public static void CreatePostDbIfNotExists(string connectionString)
    {
        SQLiteConnectionStringBuilder conStrBuilder = new(connectionString);
        if (!File.Exists(conStrBuilder.DataSource))
        {
            SQLiteConnection.CreateFile(conStrBuilder.DataSource);
            using SQLiteConnection connection = new(connectionString);
            createPostTable(connection);
#if DEBUG
            addDumpData(connection);
#endif
        }
    }

    private static void createPostTable(SQLiteConnection connection)
    {
        connection.Execute(
               sql: "CREATE TABLE IF NOT EXISTS \"Posts\" (\"PostId\" NCHAR(256) NOT NULL UNIQUE,\"PostTitle\" NVARCHAR(256) NOT NULL, \"PostDescription\" NVARCHAR(512) NOT NULL,\"CreationTime\" TEXT NOT NULL,\"UpdateTime\" TEXT, PRIMARY KEY(\"PostId\"));",
               commandTimeout: 15,
               commandType: CommandType.Text);
    }

    private static void addDumpData(SQLiteConnection connection)
    {
        DateTime creationDateTime = DateTime.Now;

        var blogPosts = new List<Post>
        {
            new() { PostId = "intro-to-aspnet-core", PostTitle = "Introduction to ASP.NET Core", PostDescription = "A comprehensive guide on getting started with ASP.NET Core.", CreationTime = creationDateTime },
            new() { PostId = "advanced-entity-framework", PostTitle = "Mastering Advanced Techniques in Entity Framework", PostDescription = "Explore performance and optimization techniques in EF Core.", CreationTime = creationDateTime },
            new() { PostId = "getting-started-with-blazor", PostTitle = "Getting Started with Blazor", PostDescription = "An introduction to Blazor for building web applications using C#.", CreationTime = creationDateTime },
            new() { PostId = "aspnet-core-vs-nodejs", PostTitle = "ASP.NET Core vs Node.js: A Comprehensive Comparison", PostDescription = "Compare ASP.NET Core and Node.js in terms of scalability, speed, and development.", CreationTime = creationDateTime },
            new() { PostId = "building-rest-apis", PostTitle = "Building REST APIs with ASP.NET Core", PostDescription = "Step-by-step guide on building REST APIs using ASP.NET Core.", CreationTime = creationDateTime },
            new() { PostId = "understanding-middleware", PostTitle = "Understanding Middleware in ASP.NET Core", PostDescription = "Deep dive into middleware and request pipeline in ASP.NET Core.", CreationTime = creationDateTime },
            new() { PostId = "implementing-dapper", PostTitle = "Implementing Dapper in ASP.NET Core", PostDescription = "Learn how to implement Dapper as a lightweight ORM in your projects.", CreationTime = creationDateTime },
            new() { PostId = "best-practices-web-api", PostTitle = "Best Practices for Web API Development", PostDescription = "Best practices to consider when developing robust Web APIs.", CreationTime = creationDateTime },
            new() { PostId = "caching-in-aspnet-core", PostTitle = "Caching Strategies in ASP.NET Core", PostDescription = "Effective caching strategies for enhancing performance in ASP.NET Core.", CreationTime = creationDateTime },
            new() { PostId = "entity-framework-vs-dapper", PostTitle = "Entity Framework vs Dapper", PostDescription = "Comparison between EF Core and Dapper for data access in .NET.", CreationTime = creationDateTime },
            new() { PostId = "asynchronous-programming-dotnet", PostTitle = "Asynchronous Programming in .NET", PostDescription = "An introduction to async and await in .NET for handling asynchronous tasks.", CreationTime = creationDateTime },
            new() { PostId = "signalr-in-realtime-apps", PostTitle = "Using SignalR for Real-Time Apps", PostDescription = "Building real-time web apps with SignalR and ASP.NET Core.", CreationTime = creationDateTime },
            new() { PostId = "dependency-injection-basics", PostTitle = "Dependency Injection in ASP.NET Core", PostDescription = "Understanding dependency injection principles and practices in ASP.NET Core.", CreationTime = creationDateTime },
            new() { PostId = "introduction-to-microservices", PostTitle = "Introduction to Microservices in ASP.NET Core", PostDescription = "A beginner's guide to microservices architecture and ASP.NET Core.", CreationTime = creationDateTime },
            new() { PostId = "identity-management-dotnet", PostTitle = "Identity Management in ASP.NET Core", PostDescription = "Implementing authentication and authorization with ASP.NET Core Identity.", CreationTime = creationDateTime },
            new() { PostId = "razor-pages-overview", PostTitle = "Overview of Razor Pages", PostDescription = "Getting started with Razor Pages as an alternative to MVC in ASP.NET Core.", CreationTime = creationDateTime },
            new() { PostId = "grpc-communication-dotnet", PostTitle = "gRPC Communication in .NET", PostDescription = "Using gRPC for efficient inter-service communication in .NET applications.", CreationTime = creationDateTime },
            new() { PostId = "cloud-deployment-aspnet-core", PostTitle = "Deploying ASP.NET Core to the Cloud", PostDescription = "A guide to deploying ASP.NET Core applications to Azure, AWS, and GCP.", CreationTime = creationDateTime },
            new() { PostId = "jwt-authentication", PostTitle = "Implementing JWT Authentication in ASP.NET Core", PostDescription = "Learn how to secure APIs using JSON Web Tokens (JWT) in ASP.NET Core.", CreationTime = creationDateTime },
            new() { PostId = "unit-testing-in-aspnet-core", PostTitle = "Unit Testing in ASP.NET Core", PostDescription = "Introduction to unit testing ASP.NET Core applications using xUnit and Moq.", CreationTime = creationDateTime },
            new() { PostId = "ci-cd-pipelines-aspnet-core", PostTitle = "CI/CD Pipelines for ASP.NET Core", PostDescription = "Setting up CI/CD pipelines with GitHub Actions, Azure DevOps, and Jenkins.", CreationTime = creationDateTime }
        };

        string insertQuery = @"
            INSERT INTO Posts (PostId, PostTitle, PostDescription, CreationTime, UpdateTime)
            VALUES (@PostId, @PostTitle, @PostDescription, @CreationTime, @UpdateTime)";

        foreach (Post post in blogPosts)
        {
            connection.Execute(insertQuery, post);
        }
    }
}
