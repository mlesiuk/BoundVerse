using BoundVerse.Application.Abstractions.Interfaces;
using BoundVerse.Infrastructure.Persistence;
using BoundVerse.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BoundVerse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql("Host=host.docker.internal;Port=5432;Database=postgres;Username=postgres;Password=example");
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }
}
