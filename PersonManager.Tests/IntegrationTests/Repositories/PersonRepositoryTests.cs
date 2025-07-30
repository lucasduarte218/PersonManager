using Microsoft.EntityFrameworkCore;
using PersonManager.Infrastructure.Data;
using PersonManager.Infrastructure.Repositories;
using PersonManager.Tests.Builders;

namespace PersonManager.Tests.IntegrationTests.Repositories;

public class PersonRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly PersonRepository _repository;

    public PersonRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new PersonRepository(_context);
    }

    [Fact]
    public async Task AddAsync_ShouldAddPersonToDatabase()
    {
        // Arrange
        var person = PersonBuilder.Create().WithValidDefaults().Build();

        // Act
        await _repository.AddAsync(person);
        await _context.SaveChangesAsync(); // Importante: Salvar as mudanças

        // Assert
        var savedPerson = await _context.Persons
            .FirstOrDefaultAsync(p => p.CPF == person.CPF && p.DeletedAt == null);
        savedPerson.Should().NotBeNull();
        savedPerson!.Name.Should().Be(person.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnPerson()
    {
        // Arrange
        var person = PersonBuilder.Create().WithValidDefaults().Build();
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(person.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(person.Id);
        result.Name.Should().Be(person.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithDeletedPerson_ShouldReturnNull()
    {
        // Arrange
        var person = PersonBuilder.Create().WithValidDefaults().Build();
        person.DeletedAt = DateTime.UtcNow;
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(person.Id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByCpfAsync_WithExistingCpf_ShouldReturnPerson()
    {
        // Arrange
        var uniqueCpf = $"{DateTime.Now.Ticks}".PadLeft(11, '0').Substring(0, 11);
        var person = PersonBuilder.Create().WithValidDefaults().WithCpf(uniqueCpf).Build();
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByCpfAsync(uniqueCpf);

        // Assert
        result.Should().NotBeNull();
        result!.CPF.Should().Be(uniqueCpf);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSetDeletedAt()
    {
        // Arrange
        var person = PersonBuilder.Create().WithValidDefaults().Build();
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(person);

        // Assert
        var deletedPerson = await _context.Persons
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == person.Id);
        deletedPerson.Should().NotBeNull();
        deletedPerson!.DeletedAt.Should().NotBeNull();
        deletedPerson.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}