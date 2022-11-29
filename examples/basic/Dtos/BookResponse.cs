using FastCrud.Examples.Basic.Entities;
using FastCrud.Kernel.Dtos;

namespace FastCrud.Examples.Basic.Dtos;

public class BookResponse : BaseResponse<Book, int>
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public override void Set(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Description = book.Description;
        CreatedAt = book.CreatedAt;
    }
}
