using BoundVerse.Application.Dtos;
using BoundVerse.Application.Exceptions;
using BoundVerse.Application.Services;
using FluentValidation;
using System.Net.Mime;

namespace BoundVerse.Api.Endpoints.Book;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapDelete("/book/{id}", async (
            string? id,
            IBookService booksService,
            CancellationToken cancellationToken = default) =>
        {
            var result = await booksService.DeleteBook(id, cancellationToken);
            return result.Match(
                success => Results.Ok(),
                invalid => Results.BadRequest(invalid),
                validationException => Results.BadRequest(validationException));
        })
        .Produces<BookDto>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)
        .Produces<InvalidInputException>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
        .Produces<ValidationException>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
        .WithName("book-delete")
        .WithOpenApi();
    }
}
