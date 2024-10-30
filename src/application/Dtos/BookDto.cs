using System.Text.Json.Serialization;

namespace BoundVerse.Application.Dtos;

public sealed record class BookDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("numberOfPages")]
    public int NumberOfPages { get; set; }

    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;
}
