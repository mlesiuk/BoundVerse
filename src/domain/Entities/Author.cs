namespace BoundVerse.Domain.Entities;

public sealed class Author : AuditableEntity
{
    private Author() { }

    public static Author CreateAuthor(string name, string surname)
    {
        return new()
        {
            Name = name,
            Surname = surname
        };
    }

    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;

    private readonly IList<Book> _books = [];
    public List<Book> Books => [.. _books];

    public void AddBook(Book book)
    {
        if (_books.Contains(book))
        {
            _books.Add(book);
        }
    }
}
