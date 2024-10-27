namespace BoundVerse.Domain.Entities;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAtUtc { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedUtc { get; set; }
    public string? LastModifiedBy { get; set; }
}
