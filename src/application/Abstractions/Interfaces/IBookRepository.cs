using BoundVerse.Domain.Entities;

namespace BoundVerse.Application.Abstractions.Interfaces;

public interface IBookRepository
{
    Task AddAsync(Book book, CancellationToken cancellationToken = default);
    Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Book book, CancellationToken cancellationToken = default);
    Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}
