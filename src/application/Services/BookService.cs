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
    Task<OneOf<int, InvalidInputException, NotFoundException>> DeleteBook(string? id, CancellationToken cancellationToken = default);
    Task<OneOf<BookDto, InvalidInputException, NotFoundException>> GetBookById(string? id, CancellationToken cancellationToken = default);
    Task<IEnumerable<BookDto>> GetAllBooks(CancellationToken cancellationToken = default);
    Task<OneOf<int, InvalidInputException, NotFoundException>> UpdateBook(string? id, BookDto? bookDto, CancellationToken cancellationToken = default);
}

public class BookService(
    IBookRepository bookRepository, 
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork, 
    IValidator<BookDto> validator) : IBookService
{
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

        var categoryFromDto = bookDto.Category;
        var category = await categoryRepository.GetByIdAsync(categoryFromDto.Id, cancellationToken);
        category ??= await categoryRepository.GetByNameAsync(categoryFromDto.Name, cancellationToken);

        if (category is null)
        {
            return new InvalidInputException($"Category does not exist.");
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

    public async Task<OneOf<int, InvalidInputException, NotFoundException>> DeleteBook(string? id, CancellationToken cancellationToken = default)
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

        await bookRepository.RemoveAsync(result, cancellationToken);
        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<BookDto>> GetAllBooks(CancellationToken cancellationToken = default)
    {
        var s = await bookRepository
            .GetAllAsync(cancellationToken);

        return s
            .Where(b => b is not null)
            .Select(b => b.Adapt<BookDto>());
    }

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

    public async Task<OneOf<int, InvalidInputException, NotFoundException>> UpdateBook(string? id, BookDto? bookDto, CancellationToken cancellationToken = default)
    {
        _ = Guid.TryParse(id, out var bookId);

        if (bookId == Guid.Empty)
        {
            return new InvalidInputException();
        }

        if (bookDto is null)
        {
            return new InvalidInputException();
        }

        var book = await bookRepository.GetByIdAsync(bookId, cancellationToken);
        if (book is null)
        {
            return new NotFoundException(nameof(Book));
        }

        var category = Category.Create(
            bookDto.Category.Name,
            bookDto.Category.Description,
            bookDto.Category.IsRoot,
            bookDto.Category.IsLeaf);

        book.UpdateBook(
            bookDto.Title,
            bookDto.Description,
            category,
            bookDto.Year,
            bookDto.NumberOfPages);

        await bookRepository.UpdateAsync(book, cancellationToken);
        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
