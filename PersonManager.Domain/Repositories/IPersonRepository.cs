using PersonManager.Domain.Entities;

namespace PersonManager.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task<Person?> GetByIdAsync(long id);
        Task<Person?> GetByCpfAsync(string cpf);
        Task<IEnumerable<Person>> GetAllAsync();
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(Person person);
    }
}