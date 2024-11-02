using BoundVerse.Application.Abstractions.Interfaces;
using BoundVerse.Application.Dtos;
using BoundVerse.Application.Exceptions;
using BoundVerse.Domain.Entities;
using FluentValidation;
using Mapster;
using OneOf;

namespace BoundVerse.Application.Services;

public interface ICategoryService
{
    Task<OneOf<CategoryDto, InvalidInputException, NotFoundException>> GetCategoryById(string? id, CancellationToken cancellationToken = default);
    Task<OneOf<CategoryDto, InvalidInputException, ValidationException>> CreateCategory(CategoryDto?  categoryDto, CancellationToken cancellationToken = default);
}

public sealed class CategoryService(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IValidator<CategoryDto> validator) : ICategoryService
{
    public async Task<OneOf<CategoryDto, InvalidInputException, ValidationException>> CreateCategory(CategoryDto? categoryDto, CancellationToken cancellationToken = default)
    {
        if (categoryDto is null)
        {
            return new InvalidInputException();
        }

        var validationResult = await validator.ValidateAsync(categoryDto, cancellationToken);
        var failures = validationResult.Errors?.ToList();
        if (failures is not null && failures!.Count != 0)
        {
            return new ValidationException(failures);
        }

        var category = Category.Create(
            categoryDto.Name,
            categoryDto.Description,
            categoryDto.IsRoot,
            categoryDto.IsLeaf);

        await categoryRepository.AddAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return category.Adapt<CategoryDto>();
    }

    public async Task<OneOf<CategoryDto, InvalidInputException, NotFoundException>> GetCategoryById(string? id, CancellationToken cancellationToken = default)
    {
        _ = Guid.TryParse(id, out var categoryId);

        if (categoryId == Guid.Empty)
        {
            return new InvalidInputException();
        }

        var result = await categoryRepository.GetByIdAsync(categoryId, cancellationToken);
        if (result is null)
        {
            return new NotFoundException(nameof(Book));
        }

        return result.Adapt<CategoryDto>();
    }
}
