using PersonManager.Domain.Entities;

namespace PersonManager.Application.Interfaces;
public interface IPersonService
{
    Task<Result<Person>> CreateAsync(Person person);
    Task<Result<Person>> UpdateAsync(Person person);
    Task<Result> DeleteAsync(long id);
    Task<Result<Person>> GetByIdAsync(long id);
    Task<Result<IEnumerable<Person>>> GetAllAsync();
}