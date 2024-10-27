using BoundVerse.Application.Abstractions.Interfaces;
using BoundVerse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BoundVerse.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        var username = _currentUserService.UserName;
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry is null)
            {
                continue;
            }

            if (entry.Entity is null)
            {
                continue;
            }

            username ??= "system";

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

        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
