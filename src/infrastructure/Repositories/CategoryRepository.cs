using BoundVerse.Application.Abstractions.Interfaces;
using BoundVerse.Domain.Entities;
using BoundVerse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BoundVerse.Infrastructure.Repositories;

public sealed class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    private readonly DbSet<Category> _categories = context.Set<Category>();

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        await _categories.AddAsync(category, cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _categories
            .Where(c => c.DeletedAtUtc == null)
            .ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _categories
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _categories
            .FirstOrDefaultAsync(c => EF.Functions.ILike(c.Name, name), cancellationToken);
    }

    public async Task RemoveAsync(Category category, CancellationToken cancellationToken = default)
    {
        var categoryToRemove = await _categories.SingleOrDefaultAsync(c => c.Id == category.Id, cancellationToken);
        if (categoryToRemove is not null)
        {
            _categories.Remove(category);
        }
    }

    public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        var categoryToUpdate = await _categories.SingleOrDefaultAsync(c => c.Id == category.Id, cancellationToken);
        if (categoryToUpdate is not null)
        {
            _categories.Update(category);
        }
    }
}
