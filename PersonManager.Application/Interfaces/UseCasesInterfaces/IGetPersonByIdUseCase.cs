using PersonManager.Domain.Entities;

namespace PersonManager.Application.Interfaces.UseCasesInterfaces;

public interface IGetPersonByIdUseCase
{
    Task<Result<Person>> ExecuteAsync(long id);
}