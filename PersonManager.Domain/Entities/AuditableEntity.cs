namespace PersonManager.Domain.Entities;

public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    protected AuditableEntity()
    {
        var now = DateTime.UtcNow;
        CreatedAt = now;
        UpdatedAt = now;
        DeletedAt = null;
    }
}