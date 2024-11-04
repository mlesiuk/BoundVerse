using BoundVerse.Application.Dtos;
using BoundVerse.Application.Services;

namespace BoundVerse.Api.Endpoints.Book;

public class Modify : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPatch("/book/{id}", async (
            string? id,
            BookDto? bookDto,
            IBookService bookService,
            CancellationToken cancellationToken = default) =>
        {
            var book = await bookService.UpdateBook(id, bookDto, cancellationToken);
            return book.Match(
                success => Results.Ok(),
                invalidInput => Results.BadRequest(invalidInput),
                notFound => Results.NotFound());
        })
        .WithName("book-modify")
        .WithOpenApi();
    }
}
