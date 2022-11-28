namespace FastCrud.Kernel.Entities;

public interface IEntity<TId>
{
    TId Id { get; set; }
}
