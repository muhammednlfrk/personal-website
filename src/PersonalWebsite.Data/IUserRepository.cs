using PersonalWebsite.Data.Entities;

namespace PersonalWebsite.Data;

public interface IUserRepository : IRepository<User>
{
    Task<User?> FindAsync(int userId);
    Task<User?> FindAsync(string username);
    Task<User?> FindAsync(string username, string password);
    Task<bool> DeleteAsync(int userId);
}
