namespace FastCrud.Kernel.Entities;

public abstract class CreateableEntity<TId> : IEntity<TId>, IHasCreatedAt
{
    public TId Id { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
}
