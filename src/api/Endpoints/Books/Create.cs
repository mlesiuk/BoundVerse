using BoundVerse.Api.Services;

namespace BoundVerse.Api.Endpoints.Books;

public class Create : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/", async(
            string? id,
            IBooksService booksService,
            CancellationToken cancellationToken = default) =>
            {
                var book = await booksService.GetBookById(id, cancellationToken);
                return Results.Ok();
            })
        .WithName("book")
        .WithOpenApi();
    }
}
