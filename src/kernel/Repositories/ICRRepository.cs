using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Repositories;

public interface ICRRepository<TEntity, TId> : IRRepository<TEntity, TId>, ICRepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
}
