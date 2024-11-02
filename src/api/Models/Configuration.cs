namespace BoundVerse.Api.Models;

public sealed record class Configuration
{
    public Database Database { get; set; } = new();
}

public sealed record class Database
{
    public string ConnectionString { get; set; } = string.Empty;
}
