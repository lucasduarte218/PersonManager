using PersonManager.Application.Interfaces.UseCasesInterfaces;
using PersonManager.Domain.Entities;
using PersonManager.Domain.Repositories;

namespace PersonManager.Application.UseCases;

public class GetAllPersonsUseCase : IGetAllPersonsUseCase
{
    private readonly IPersonRepository _repository;

    public GetAllPersonsUseCase(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<Person>>> ExecuteAsync()
    {
        var people = await _repository.GetAllAsync();
        return Result<IEnumerable<Person>>.Success(people);
    }
}