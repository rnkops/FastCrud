using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Repositories;

public interface IRURepository<TEntity, TId> : IRRepository<TEntity, TId>, IURepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
}
