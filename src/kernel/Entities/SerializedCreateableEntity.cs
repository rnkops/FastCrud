namespace FastCrud.Kernel.Entities;

public abstract class SerializedCreateableEntity<TId> : IEntity<TId>, IHasCreatedAt, IHasSerial
{
    public TId Id { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public long Serial { get; set; }
}
