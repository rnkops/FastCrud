using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Repositories;

public interface ICRepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
    void Add(TEntity entity);
    void Add(IEnumerable<TEntity> entities);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
