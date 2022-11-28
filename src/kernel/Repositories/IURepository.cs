using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Repositories;

public interface IURepository<TEntity, TId> where TEntity : IEntity<TId>
{
    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task UpdateAsync(IEnumerable<TEntity> entities);
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
