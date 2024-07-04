using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManningBooksApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly CatalogContext _dbContext;

    public CatalogController(
      CatalogContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IAsyncEnumerable<Book> GetBooks(
        string? titleFilter = null)
    {
        IQueryable<Book> query = _dbContext.Books
          .Include(b => b.Ratings)
          .AsNoTracking();
        if (titleFilter != null)
        {
            query = query.Where(b =>
              b.Title.ToLower().Contains(titleFilter.ToLower()));
        }

        return query.AsAsyncEnumerable();
    }

    // HTTP GET method to request a single Book object by id

    [HttpGet("{id}")]
    public Task<Book?> GetBook(int id)
    {
        return _dbContext.Books.FirstOrDefaultAsync(
          b => b.Id == id);
    }

    /* BookCreateCommand and BookUpdateCommand 
    are inner records in the CatalogController 
    that control how Book entities are created and updated
    */

    public record BookCreateCommand(
    string Title, string? Description)
    { }
    public record BookUpdateCommand(
      string? Title, string? Description)
    { }

    // POST method to create a new Book

    [HttpPost]
    public async Task<Book> CreateBookAsync(BookCreateCommand command,
    CancellationToken cancellationToken)
    {
        var book = new Book(
            command.Title,
            command.Description
        );

        var entity = _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }

    // PATCH method to make a partial update on an existing Book

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateBookAsync(
        int id, BookUpdateCommand command,
        CancellationToken cancellationToken
    )
    {
        var book = await _dbContext.FindAsync<Book>(
            new object?[] { id },
            cancellationToken
        );
        if (book == null)
        {
            return NotFound();
        }
        if (command.Title != null)
        {
            book.Title = command.Title;
        }
        if (command.Description != null)
        {
            book.Description = command.Description;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    // DELETE method to delete a Book

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> DeleteBookAsync(int id, CancellationToken cancellationToken)
    {
        // Include the ratings with the book so that the DeleteBook method can perform a cascade delete
        var book = await _dbContext.Books.Include(b => b.Ratings)
            .FirstOrDefaultAsync(b => b.Id == id,
            cancellationToken);
        if (book == null)
        {
            return NotFound();
        }

        _dbContext.Remove(book);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

}