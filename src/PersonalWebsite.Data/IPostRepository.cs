using PersonalWebsite.Data.Entities;

namespace PersonalWebsite.Data;

public interface IPostRepository : IRepository<Post>, IPagedRepository<Post>
{
    Task<Post?> GetAsync(string postId);
    Task<bool> DeleteAsync(string postId);
    Task<IEnumerable<Post>> GetLast10ArticesAsync();
}
