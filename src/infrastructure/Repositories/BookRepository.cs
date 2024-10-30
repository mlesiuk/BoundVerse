using BoundVerse.Application.Abstractions.Interfaces;
using BoundVerse.Domain.Entities;
using BoundVerse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BoundVerse.Infrastructure.Repositories;

public class BookRepository(ApplicationDbContext context) : IBookRepository
{
    private readonly DbSet<Book> _books = context.Set<Book>();

    public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        await _books.AddAsync(book, cancellationToken);
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _books.FindAsync([id], cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        var bookToUpdate = await _books.SingleOrDefaultAsync(b => b.Id == book.Id, cancellationToken);
        if (bookToUpdate is not null)
        {
            _books.Update(book);
        }
    }

    public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await _books.FindAsync([id], cancellationToken: cancellationToken);
        if (book is not null)
        {
            _books.Remove(book);
        }
    }
}
