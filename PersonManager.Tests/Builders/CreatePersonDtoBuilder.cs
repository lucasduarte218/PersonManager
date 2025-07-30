using PersonManager.Application.DTOs;

namespace PersonManager.Tests.Builders;

public class CreatePersonDtoBuilder
{
    private CreatePersonDto _dto = new();

    public static CreatePersonDtoBuilder Create() => new();

    public CreatePersonDtoBuilder WithName(string name)
    {
        _dto.Name = name;
        return this;
    }

    public CreatePersonDtoBuilder WithCpf(string cpf)
    {
        _dto.CPF = cpf;
        return this;
    }

    public CreatePersonDtoBuilder WithEmail(string email)
    {
        _dto.Email = email;
        return this;
    }

    public CreatePersonDtoBuilder WithBirthDate(DateTime birthDate)
    {
        _dto.BirthDate = birthDate;
        return this;
    }

    public CreatePersonDtoBuilder WithValidDefaults()
    {
        _dto.Name = "João Silva";
        _dto.CPF = "12345678901";
        _dto.Email = "joao@email.com";
        _dto.BirthDate = new DateTime(1990, 1, 1);
        _dto.Gender = "M";
        _dto.PlaceOfBirth = "São Paulo";
        _dto.Nationality = "Brasileiro";
        return this;
    }

    public CreatePersonDto Build() => _dto;
}