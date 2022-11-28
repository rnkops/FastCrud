using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Repositories;

public interface ICRURepository<TEntity, TId> : IRRepository<TEntity, TId>, ICRepository<TEntity, TId>, ICRRepository<TEntity, TId>, IURepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
}
