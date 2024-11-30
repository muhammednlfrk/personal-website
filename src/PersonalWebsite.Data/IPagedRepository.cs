using PersonalWebsite.Data.Entities;

namespace PersonalWebsite.Data;

public interface IPagedRepository<TEntity> where TEntity : EntityBase
{
    Task<IEnumerable<TEntity>> GetPagedAsync(uint page, uint pageSize);
}
