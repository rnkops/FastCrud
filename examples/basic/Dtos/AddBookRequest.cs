using FastCrud.Examples.Basic.Entities;
using FastCrud.Kernel.Dtos;

namespace FastCrud.Examples.Basic.Dtos;

public class AddBookRequest : BaseCreateRequest<Book, int>
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public override Book ToEntity()
        => new()
        {
            CreatedAt = DateTimeOffset.UtcNow,
            Description = Description?.Trim(),
            Title = Title.Trim(),
        };
}
