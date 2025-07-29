namespace PersonManager.Domain.Entities;

public class User : AuditableEntity
{
    public long Id { get; set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Role { get; set; } = "User";
}