using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Repositories;

public interface IRDRepository<TEntity, TId> : IRRepository<TEntity, TId>, IDRepository<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
{
}
