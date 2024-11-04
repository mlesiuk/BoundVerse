using BoundVerse.Application.Services;

namespace BoundVerse.Api.Endpoints.Book;

public class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/book", async (
            IBookService bookService,
            CancellationToken cancellationToken = default) =>
        {
            return await bookService.GetAllBooks(cancellationToken);
        })
        .WithName("books")
        .WithOpenApi();
    }
}
