using PersonManager.Application.DTOs;
using PersonManager.Domain.Entities;

namespace PersonManager.Application.Interfaces.UseCasesInterfaces;

public interface ICreatePersonUseCase
{
    Task<Result<Person>> ExecuteAsync(CreatePersonDto dto);
}