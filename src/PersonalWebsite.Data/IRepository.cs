using PersonalWebsite.Data.Entities;

namespace PersonalWebsite.Data;
public interface IRepository<TEntity> where TEntity : EntityBase
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetPagedAsync(uint page, uint pageSize);
    Task<TEntity?> CreateAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
}
