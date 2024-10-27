using BoundVerse.Api.Models;

namespace BoundVerse.Api.Services;

public interface IBooksService
{
    Task<Book> GetBookById(string id, CancellationToken cancellationToken = default);
}

public class BooksService : IBooksService
{
    public BooksService()
    {
    }

    public async Task<Book> GetBookById(string id, CancellationToken cancellationToken = default)
    {
        return new();
    }
}
