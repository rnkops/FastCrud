namespace FastCrud.Kernel.Entities;

public abstract class Entity<TId> : IEntity<TId>
{
    public TId Id { get; set; } = default!;
}
