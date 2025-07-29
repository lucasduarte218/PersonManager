using PersonManager.Domain.Entities;

namespace PersonManager.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
}