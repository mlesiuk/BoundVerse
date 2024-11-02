namespace BoundVerse.Domain.Entities;

public sealed class Category : AuditableEntity
{
    private Category() { }

    public static Category Create(
        string name,
        string description,
        bool isRoot,
        bool isLeaf)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        return new Category
        {
            Name = name,
            Description = description,
            IsRoot = isRoot,
            IsLeaf = isLeaf
        };
    }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsRoot { get; set; }

    public bool IsLeaf { get; set; }

    public Category? Parent { get; set; }

    public IEnumerable<Category> Children { get; } = [];
}
