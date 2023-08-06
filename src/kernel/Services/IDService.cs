using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Services;

public interface IDService<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
{
    void Delete(TEntity entity);
    void Delete(IEnumerable<TEntity> entities);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    void Remove(TEntity entity);
    void Remove(IEnumerable<TEntity> entities);
    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
