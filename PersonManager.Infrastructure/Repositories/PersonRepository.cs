using Microsoft.EntityFrameworkCore;
using PersonManager.Domain.Entities;
using PersonManager.Domain.Repositories;
using PersonManager.Infrastructure.Data;

namespace PersonManager.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _context;
    public PersonRepository(ApplicationDbContext context) => _context = context;

    public async Task<Person?> GetByIdAsync(long id) =>
        await _context.Persons.FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null);

    public async Task<Person?> GetByCpfAsync(string cpf) =>
        await _context.Persons.FirstOrDefaultAsync(p => p.CPF == cpf && p.DeletedAt == null);

    public async Task<IEnumerable<Person>> GetAllAsync() =>
        await _context.Persons.Where(p => p.DeletedAt == null).ToListAsync();

    public async Task AddAsync(Person person)
    {
        await _context.Persons.AddAsync(person);
    }

    public async Task UpdateAsync(Person person)
    {
        _context.Persons.Update(person);
    }

    public async Task DeleteAsync(Person person)
    {
        person.DeletedAt = DateTime.UtcNow;
        _context.Persons.Update(person);
    }
}