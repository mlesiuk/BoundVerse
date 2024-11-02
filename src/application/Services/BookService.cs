using BoundVerse.Application.Abstractions.Interfaces;
using BoundVerse.Application.Dtos;
using BoundVerse.Application.Exceptions;
using BoundVerse.Domain.Entities;
using FluentValidation;
using Mapster;
using OneOf;

namespace BoundVerse.Application.Services;

public interface IBookService
{
    Task<OneOf<BookDto, InvalidInputException, ValidationException>> CreateBook(BookDto? bookDto, CancellationToken cancellationToken = default);
    Task<OneOf<BookDto, InvalidInputException, NotFoundException>> GetBookById(string? id, CancellationToken cancellationToken = default);
}

public class BookService(
    IBookRepository bookRepository, 
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork, 
    IValidator<BookDto> validator) : IBookService
{
    public async Task<OneOf<BookDto, InvalidInputException, NotFoundException>> GetBookById(string? id, CancellationToken cancellationToken = default)
    {
        _ = Guid.TryParse(id, out var bookId);

        if (bookId == Guid.Empty)
        {
            return new InvalidInputException();
        }

        var result = await bookRepository.GetByIdAsync(bookId, cancellationToken);
        if (result is null)
        {
            return new NotFoundException(nameof(Book));
        }

        return result.Adapt<BookDto>();
    }

    public async Task<OneOf<BookDto, InvalidInputException, ValidationException>> CreateBook(BookDto? bookDto, CancellationToken cancellationToken = default)
    {
        if (bookDto is null)
        {
            return new InvalidInputException();
        }
     
        var validationResult = await validator.ValidateAsync(bookDto, cancellationToken);
        var failures = validationResult.Errors?.ToList();
        if (failures is not null && failures!.Count != 0)
        {
            return new ValidationException(failures);
        }

        _ = Guid.TryParse(bookDto.CategoryId,out var categoryId);
        var category = await categoryRepository.GetByIdAsync(categoryId, cancellationToken);
        if (category is null)
        {
            return new InvalidInputException($"Category with id {bookDto.CategoryId} does not exists.");
        }

        var book = Book.CreateBook(
            bookDto.Title,
            bookDto.Description,
            category,
            bookDto.Year,
            bookDto.NumberOfPages);

        await bookRepository.AddAsync(book, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return book.Adapt<BookDto>();
    }
}
