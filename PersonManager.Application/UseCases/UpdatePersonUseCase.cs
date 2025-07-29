using PersonManager.Application.DTOs;
using PersonManager.Application.Interfaces.UseCasesInterfaces;
using PersonManager.Domain.Entities;
using PersonManager.Domain.Repositories;

namespace PersonManager.Application.UseCases;

public class UpdatePersonUseCase : IUpdatePersonUseCase
{
    private readonly IPersonRepository _repository;

    public UpdatePersonUseCase(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Person>> ExecuteAsync(UpdatePersonDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null)
            return Result<Person>.Failure("Pessoa n�o encontrada.");

        if (string.IsNullOrWhiteSpace(dto.Name))
            return Result<Person>.Failure("Nome � obrigat�rio.");

        if (dto.BirthDate == default)
            return Result<Person>.Failure("Data de nascimento � obrigat�ria.");

        if (string.IsNullOrWhiteSpace(dto.CPF))
            return Result<Person>.Failure("CPF � obrigat�rio.");

        var cpfOwner = await _repository.GetByCpfAsync(dto.CPF);
        if (cpfOwner != null && cpfOwner.Id != dto.Id)
            return Result<Person>.Failure("CPF j� cadastrado para outra pessoa.");

        if (!string.IsNullOrWhiteSpace(dto.Email) && !Helpers.ValidationHelper.IsValidEmail(dto.Email))
            return Result<Person>.Failure("E-mail inv�lido.");

        if (!Helpers.ValidationHelper.IsValidCpf(dto.CPF))
            return Result<Person>.Failure("CPF inv�lido.");

        existing.Name = dto.Name;
        existing.Gender = dto.Gender;
        existing.Email = dto.Email;
        existing.BirthDate = dto.BirthDate;
        existing.PlaceOfBirth = dto.PlaceOfBirth;
        existing.Nationality = dto.Nationality;
        existing.CPF = dto.CPF;
        existing.Address = dto.Address;
        existing.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existing);

        return Result<Person>.Success(existing);
    }
}