using Microsoft.EntityFrameworkCore;

namespace BoundVerse.Application.Abstractions.Interfaces;

public interface IApplicationDbContext : IDisposable
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
