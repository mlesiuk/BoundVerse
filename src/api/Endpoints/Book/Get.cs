using BoundVerse.Application.Dtos;
using BoundVerse.Application.Exceptions;
using BoundVerse.Application.Services;
using System.Net.Mime;

namespace BoundVerse.Api.Endpoints.Book;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/book/{id}", async (
            string? id,
            IBookService booksService,
            CancellationToken cancellationToken = default) =>
        {
            var book = await booksService.GetBookById(id, cancellationToken);
            return book.Match(
                success => Results.Ok(book.Value),
                invalidInput => Results.BadRequest(invalidInput),
                notFound => Results.NotFound());
        })
        .Produces<BookDto>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
        .Produces<InvalidInputException>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status404NotFound)
        .WithName("book")
        .WithOpenApi();
    }
}
