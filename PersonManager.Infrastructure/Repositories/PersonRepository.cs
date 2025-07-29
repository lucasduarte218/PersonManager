using Microsoft.EntityFrameworkCore;
using PersonManager.Domain.Entities;
using PersonManager.Domain.Repositories;

namespace PersonManager.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly Data.ApplicationDbContext _context;
    public PersonRepository(Data.ApplicationDbContext context) => _context = context;

    public async Task<Person?> GetByIdAsync(long id) =>
        await _context.Persons.FindAsync(id);

    public async Task<Person?> GetByCpfAsync(string cpf) =>
        await _context.Persons.FirstOrDefaultAsync(p => p.CPF == cpf);

    public async Task<IEnumerable<Person>> GetAllAsync() =>
        await _context.Persons.ToListAsync();

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
        _context.Persons.Remove(person);
    }
}