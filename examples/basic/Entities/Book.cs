using FastCrud.Kernel.Entities;

namespace FastCrud.Examples.Basic.Entities;

public class Book : IDeletableEntity<int>, IHasCreatedAt
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
