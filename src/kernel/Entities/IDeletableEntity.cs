namespace FastCrud.Kernel.Entities;

public interface IDeletableEntity<TId> : IEntity<TId>
{
    DateTimeOffset? DeletedAt { get; set; }
}
