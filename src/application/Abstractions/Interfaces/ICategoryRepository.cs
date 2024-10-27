using BoundVerse.Application.Abstractions.Interfaces;
using BoundVerse.Domain.Entities;

namespace BoundVerse.Application.Abstractions.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category?> FindByNameAsync(string name);
}
