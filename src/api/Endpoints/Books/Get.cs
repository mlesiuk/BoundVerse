using BoundVerse.Api.Services;

namespace BoundVerse.Api.Endpoints.Books;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/books", async (
            string? id,
            IBooksService booksService,
            CancellationToken cancellationToken = default) =>
        {
            var book = await booksService.GetBookById(id, cancellationToken);
            return book.Match(
                success => Results.Ok(book.Value),
                invalidInput => Results.BadRequest(invalidInput),
                notFound => Results.NotFound());
        })
        .WithName("book")
        .WithOpenApi();
    }
}
