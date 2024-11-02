using BoundVerse.Application.Dtos;
using BoundVerse.Application.Exceptions;
using BoundVerse.Application.Services;
using FluentValidation;
using System.Net.Mime;

namespace BoundVerse.Api.Endpoints.Category;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/category", async(
            CategoryDto? categoryDto,
            ICategoryService categoryService,
            CancellationToken cancellationToken = default) =>
            {
                var result = await categoryService.CreateCategory(categoryDto, cancellationToken);
                return result.Match(
                    success => Results.Created("/category", success),
                    invalid => Results.BadRequest(invalid),
                    validationException => Results.BadRequest(validationException));
            })
        .Produces<BookDto>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)
        .Produces<InvalidInputException>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
        .Produces<ValidationException>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
        .WithOpenApi();
    }
}
