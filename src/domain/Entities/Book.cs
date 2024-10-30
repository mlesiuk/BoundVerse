
namespace BoundVerse.Domain.Entities;

public sealed class Book : AuditableEntity
{
    private Book() { }

    public static Book CreateBook(
        string title,
        string description,
        int year, 
        int numberOfPages)
    {
        return new Book()
        {
            Title = title,
            Description = description,
            Year = year,
            NumberOfPages = numberOfPages
        };
    }

    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int NumberOfPages { get; private set; }
    public int Year { get; private set; }
    public Category? Category { get; private set; }

    private readonly List<Author> _authors = [];
    public List<Author> Author => _authors;

    public void AddAuthor(Author author)
    {
        if (_authors.Contains(author)) return;

        _authors.Add(author);
    }
}
