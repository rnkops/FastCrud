using FastCrud.Examples.Basic.Dtos;
using FastCrud.Examples.Basic.Entities;
using FastCrud.Examples.Basic.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastCrud.Examples.Basic.Controllers;

[ApiController]
[Route("/books")]
public class BookController : ControllerBase
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    public async Task<ActionResult<BookResponse>> AddAsync([FromBody] AddBookRequest addBookRequest)
    {
        var response = await _bookService.AddAsync<BookResponse, AddBookRequest>(addBookRequest);
        _ = await _bookService.SaveChangesAsync();
        return Created("", response);
    }

    [HttpGet]
    public async Task<ActionResult<BookResponse[]>> GetAsync([FromQuery] GetBooksQuery getBooksQuery)
    {
        var responses = await _bookService.GetAsync<BookResponse, GetBooksQuery>(getBooksQuery);
        return Ok(responses);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookResponse>> GetAsync(int id)
    {
        var response = await _bookService.FindAsync<BookResponse>(id);
        if (response is null)
            return NotFound();
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _bookService.DeleteAsync(id);
        _ = await _bookService.SaveChangesAsync();
        return NoContent();
    }
}
