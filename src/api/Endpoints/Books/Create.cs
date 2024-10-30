using BoundVerse.Api.Services;
using BoundVerse.Application.Dtos;

namespace BoundVerse.Api.Endpoints.Books;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/books", async(
            BookDto? bookDto,
            IBooksService booksService,
            CancellationToken cancellationToken = default) =>
            {
                var result = await booksService.CreateBook(bookDto, cancellationToken);
                return result.Match(
                    success => Results.Created("/books", success),
                    invalid => Results.BadRequest(invalid));
            })
        .WithOpenApi();
    }
}
