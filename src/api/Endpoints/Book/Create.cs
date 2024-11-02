using BoundVerse.Application.Dtos;
using BoundVerse.Application.Exceptions;
using BoundVerse.Application.Services;
using FluentValidation;
using System.Net.Mime;

namespace BoundVerse.Api.Endpoints.Book;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/book", async(
            BookDto? bookDto,
            IBookService booksService,
            CancellationToken cancellationToken = default) =>
            {
                var result = await booksService.CreateBook(bookDto, cancellationToken);
                return result.Match(
                    success => Results.Created("/book", success),
                    invalid => Results.BadRequest(invalid),
                    validationException => Results.BadRequest(validationException));
            })
        .Produces<BookDto>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)
        .Produces<InvalidInputException>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
        .Produces<ValidationException>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
        .WithName("book-create")
        .WithOpenApi();
    }
}
