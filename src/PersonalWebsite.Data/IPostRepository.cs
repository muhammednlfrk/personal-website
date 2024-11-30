using PersonalWebsite.Data.Entities;

namespace PersonalWebsite.Data;

public interface IPostRepository : IRepository<Post>
{
    Task<Post?> GetAsync(string postId);
    Task<bool> DeleteAsync(string postId);
}
