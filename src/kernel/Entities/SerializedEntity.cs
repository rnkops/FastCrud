namespace FastCrud.Kernel.Entities;

public abstract class SerializedEntity<TId> : IEntity<TId>, IHasSerial
{
    public TId Id { get; set; } = default!;
    public long Serial { get; set; }
}
