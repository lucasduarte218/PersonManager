using PersonManager.Domain.Entities;
using PersonManager.Domain.Repositories;
using PersonManager.Infrastructure.Data;

namespace PersonManager.Infrastructure.Repositories
{
    public class RequestResponseLogRepository : IRequestResponseLogRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestResponseLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RequestResponseLog log)
        {
            await _context.Set<RequestResponseLog>().AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}