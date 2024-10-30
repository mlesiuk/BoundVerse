using BoundVerse.Application.Abstractions.Interfaces;
using BoundVerse.Application.Dtos;
using BoundVerse.Application.Exceptions;
using BoundVerse.Domain.Entities;
using Mapster;
using OneOf;

namespace BoundVerse.Api.Services;

public interface IBooksService
{
    Task<OneOf<Guid, InvalidInputException>> CreateBook(BookDto? bookDto, CancellationToken cancellationToken = default);
    Task<OneOf<BookDto, InvalidInputException, NotFoundException>> GetBookById(string? id, CancellationToken cancellationToken = default);
}

public class BooksService(IBookRepository bookRepository, IUnitOfWork unitOfWork) : IBooksService
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

    public async Task<OneOf<Guid, InvalidInputException>> CreateBook(BookDto? bookDto, CancellationToken cancellationToken = default)
    {
        if (bookDto is null)
        {
            return new InvalidInputException();
        }

        var book = Book.CreateBook(
            bookDto.Title,
            bookDto.Description,
            bookDto.Year,
            bookDto.NumberOfPages);

        await bookRepository.AddAsync(book, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return book.Id;
    }
}
