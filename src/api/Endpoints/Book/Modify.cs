using BoundVerse.Application.Services;

namespace BoundVerse.Api.Endpoints.Book;

public class Modify : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPut("/book", async (
            string? id,
            IBookService bookService,
            CancellationToken cancellationToken = default) =>
        {
            var book = await bookService.GetBookById(id, cancellationToken);
            return book.Match(
                success => Results.Ok(book.Value),
                invalidInput => Results.BadRequest(invalidInput),
                notFound => Results.NotFound());
        })
        .WithName("book-modify")
        .WithOpenApi();
    }
}
