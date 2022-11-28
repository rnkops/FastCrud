namespace FastCrud.Kernel.Entities;

public interface IHasCreatedAt
{
    DateTimeOffset CreatedAt { get; set; }
}
