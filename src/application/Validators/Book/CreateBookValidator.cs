using BoundVerse.Application.Dtos;
using FluentValidation;

namespace BoundVerse.Application.Validators.Book;

public sealed class CreateBookValidator : AbstractValidator<BookDto>
{
    public CreateBookValidator()
    {
        RuleFor(book => book.Title)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(255);

        RuleFor(book => book.Description)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(4000);

        RuleFor(book => book.Category)
            .NotNull()
            .NotEmpty();
    }
}
