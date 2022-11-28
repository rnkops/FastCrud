using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Services;

public interface IDService<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
{
    void Delete(TEntity entity);
    void Delete(IEnumerable<TEntity> entities);
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void Remove(IEnumerable<TEntity> entities);
    Task RemoveAsync(TEntity entity);
    Task RemoveAsync(IEnumerable<TEntity> entities);
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
