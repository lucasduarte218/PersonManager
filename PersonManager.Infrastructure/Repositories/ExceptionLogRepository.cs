using PersonManager.Domain.Entities;
using PersonManager.Domain.Repositories;
using PersonManager.Infrastructure.Data;

namespace PersonManager.Infrastructure.Repositories;

public class ExceptionLogRepository : IExceptionLogRepository
{
    private readonly ApplicationDbContext _context;

    public ExceptionLogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> AddAsync(ExceptionLog log)
    {
        var entry = await _context.Set<ExceptionLog>().AddAsync(log);
        await _context.SaveChangesAsync();
        return entry.Entity.Id;
    }
}