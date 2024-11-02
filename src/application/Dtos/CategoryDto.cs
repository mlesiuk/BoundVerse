using System.Text.Json.Serialization;

namespace BoundVerse.Application.Dtos;

public sealed record class CategoryDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = Guid.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("isLeaf")]
    public bool IsLeaf { get; set; }

    [JsonPropertyName("isRoot")]
    public bool IsRoot { get; set; }
}
