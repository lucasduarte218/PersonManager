using PersonManager.Domain.Entities;

namespace PersonManager.Domain.Repositories
{
    public interface IRequestResponseLogRepository
    {
        Task AddAsync(RequestResponseLog log);
    }
}