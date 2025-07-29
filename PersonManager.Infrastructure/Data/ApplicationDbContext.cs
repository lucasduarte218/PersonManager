using Microsoft.EntityFrameworkCore;
using PersonManager.Domain.Entities;

namespace PersonManager.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ExceptionLog> ExceptionLogs { get; set; }
    public DbSet<RequestResponseLog> RequestResponseLogs { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<User> Users { get; set; }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();
        var now = DateTime.UtcNow;
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}