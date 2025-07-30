using PersonManager.Domain.Entities;

namespace PersonManager.Tests.Builders;

public class PersonBuilder
{
    private Person _person = new();

    public static PersonBuilder Create() => new();

    public PersonBuilder WithId(long id)
    {
        _person.Id = id;
        return this;
    }

    public PersonBuilder WithName(string name)
    {
        _person.Name = name;
        return this;
    }

    public PersonBuilder WithCpf(string cpf)
    {
        _person.CPF = cpf;
        return this;
    }

    public PersonBuilder WithEmail(string email)
    {
        _person.Email = email;
        return this;
    }

    public PersonBuilder WithBirthDate(DateTime birthDate)
    {
        _person.BirthDate = birthDate;
        return this;
    }

    public PersonBuilder WithAddress(string address)
    {
        _person.Address = address;
        return this;
    }

    public PersonBuilder WithValidDefaults()
    {
        _person.Name = "João Silva";
        _person.CPF = "12345678901";
        _person.Email = "joao@email.com";
        _person.BirthDate = new DateTime(1990, 1, 1);
        _person.Gender = "M";
        _person.PlaceOfBirth = "São Paulo";
        _person.Nationality = "Brasileiro";
        return this;
    }

    public Person Build() => _person;
}