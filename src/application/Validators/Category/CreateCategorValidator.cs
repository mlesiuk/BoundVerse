using BoundVerse.Application.Dtos;
using FluentValidation;

namespace BoundVerse.Application.Validators.Category;

public sealed class CreateCategorValidator : AbstractValidator<CategoryDto>
{
    public CreateCategorValidator()
    {
        RuleFor(category => category.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(64);
    }
}
