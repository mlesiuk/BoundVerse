using BoundVerse.Application.Dtos;
using BoundVerse.Application.Services;
using BoundVerse.Application.Validators.Book;
using BoundVerse.Application.Validators.Category;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BoundVerse.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidator<BookDto>, CreateBookValidator>();
        services.AddScoped<IValidator<CategoryDto>, CreateCategorValidator>();

        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}
