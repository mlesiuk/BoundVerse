using BoundVerse.Application.Abstractions.Interfaces;
using BoundVerse.Domain.Entities;
using BoundVerse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BoundVerse.Infrastructure.Repositories;

public sealed class UnitOfWork(
    ApplicationDbContext context, 
    ICurrentUserService currentUserService) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var username = currentUserService.UserName ?? "system";
        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry is null)
            {
                continue;
            }

            if (entry.Entity is null)
            {
                continue;
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = username;
                    entry.Entity.CreatedAtUtc = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = username;
                    entry.Entity.LastModifiedUtc = DateTime.UtcNow;
                    break;
            }
        }
        return await context.SaveChangesAsync(cancellationToken);
    }
}
