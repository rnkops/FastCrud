using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Dtos;

public abstract class BaseDeleteRequest<TEntity, TId> where TEntity : IEntity<TId>
{
    public TId Id { get; set; } = default!;

    public abstract void DeleteEntity(TEntity entity);
}
