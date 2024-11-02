using BoundVerse.Api.Services;
using BoundVerse.Application.Abstractions.Interfaces;

namespace BoundVerse.Api.Extensions;

public static class ApiExtension
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
