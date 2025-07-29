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
                return Result<Person>.Failure("Nome é obrigatório.");

            if (dto.BirthDate == default)
                return Result<Person>.Failure("Data de nascimento é obrigatória.");

            if (string.IsNullOrWhiteSpace(dto.CPF))
                return Result<Person>.Failure("CPF é obrigatório.");

            if (!string.IsNullOrWhiteSpace(dto.Email) && !Helpers.ValidationHelper.IsValidEmail(dto.Email))
                return Result<Person>.Failure("E-mail inválido.");

            if (!Helpers.ValidationHelper.IsValidCpf(dto.CPF))
                return Result<Person>.Failure("CPF inválido.");

            var existing = await _repository.GetByCpfAsync(dto.CPF);
            if (existing != null)
                return Result<Person>.Failure("CPF já cadastrado.");

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