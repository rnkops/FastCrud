using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Repositories;

public interface IDRepository<TEntity, TId> : IURepository<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
{
    void Remove(TEntity entity);
    void Remove(IEnumerable<TEntity> entities);
    Task RemoveAsync(TEntity entity);
    Task RemoveAsync(IEnumerable<TEntity> entities);
}
