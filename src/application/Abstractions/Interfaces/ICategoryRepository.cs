using BoundVerse.Domain.Entities;

namespace BoundVerse.Application.Abstractions.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
