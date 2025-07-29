using PersonManager.Domain.Entities;

namespace PersonManager.Domain.Repositories;

public interface IExceptionLogRepository
{
    Task<long> AddAsync(ExceptionLog log);
}