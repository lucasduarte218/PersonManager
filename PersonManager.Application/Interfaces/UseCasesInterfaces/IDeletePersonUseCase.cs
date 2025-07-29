using PersonManager.Domain.Entities;

namespace PersonManager.Application.Interfaces.UseCasesInterfaces;

public interface IDeletePersonUseCase
{
    Task<Result> ExecuteAsync(long id);
}