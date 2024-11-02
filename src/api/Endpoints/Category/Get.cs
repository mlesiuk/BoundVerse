using BoundVerse.Application.Dtos;
using BoundVerse.Application.Exceptions;
using BoundVerse.Application.Services;
using System.Net.Mime;

namespace BoundVerse.Api.Endpoints.Category;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/category/{id}", async (
            string? id,
            ICategoryService categoryService,
            CancellationToken cancellationToken = default) =>
        {
            var book = await categoryService.GetCategoryById(id, cancellationToken);
            return book.Match(
                success => Results.Ok(book.Value),
                invalidInput => Results.BadRequest(invalidInput),
                notFound => Results.NotFound());
        })
        .Produces<CategoryDto>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
        .Produces<InvalidInputException>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status404NotFound)
        .WithName("category")
        .WithOpenApi();
    }
}
