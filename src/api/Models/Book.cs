namespace BoundVerse.Api.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public string Brand { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public DateTime LastUpdated { get; set; }
}
