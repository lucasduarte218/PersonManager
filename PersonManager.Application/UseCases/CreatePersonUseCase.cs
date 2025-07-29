using PersonManager.Application.DTOs;
using PersonManager.Application.Interfaces.UseCasesInterfaces;
using PersonManager.Domain.Entities;
using PersonManager.Domain.Repositories;

namespace PersonManager.Application.UseCases
{
    public class CreatePersonUseCase : ICreatePersonUseCase
    {
        private readonly IPersonRepository _repository;

        public CreatePersonUseCase(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Person>> ExecuteAsync(CreatePersonDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return Result<Person>.Failure("Nome � obrigat�rio.");

            if (dto.BirthDate == default)
                return Result<Person>.Failure("Data de nascimento � obrigat�ria.");

            if (string.IsNullOrWhiteSpace(dto.CPF))
                return Result<Person>.Failure("CPF � obrigat�rio.");

            if (!string.IsNullOrWhiteSpace(dto.Email) && !Helpers.ValidationHelper.IsValidEmail(dto.Email))
                return Result<Person>.Failure("E-mail inv�lido.");

            if (!Helpers.ValidationHelper.IsValidCpf(dto.CPF))
                return Result<Person>.Failure("CPF inv�lido.");

            var existing = await _repository.GetByCpfAsync(dto.CPF);
            if (existing != null)
                return Result<Person>.Failure("CPF j� cadastrado.");

            var person = new Person
            {
                Name = dto.Name,
                Gender = dto.Gender,
                Email = dto.Email,
                BirthDate = dto.BirthDate,
                PlaceOfBirth = dto.PlaceOfBirth,
                Nationality = dto.Nationality,
                CPF = dto.CPF,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(person);

            return Result<Person>.Success(person);
        }
    }
}