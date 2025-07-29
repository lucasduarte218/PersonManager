using PersonManager.Domain.Entities;

namespace PersonManager.Application.Interfaces.UseCasesInterfaces;

public interface IGetAllPersonsUseCase
{
    Task<Result<IEnumerable<Person>>> ExecuteAsync();
}