using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Dtos;

public abstract class BaseUpdateRequest<TEntity, TId> where TEntity : IEntity<TId>
{
    public TId Id { get; set; } = default!;

    public abstract void UpdateEntity(TEntity entity);
}
