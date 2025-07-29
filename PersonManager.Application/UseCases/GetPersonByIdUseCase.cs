using PersonManager.Application.Interfaces.UseCasesInterfaces;
using PersonManager.Domain.Entities;
using PersonManager.Domain.Repositories;

namespace PersonManager.Application.UseCases;

public class GetPersonByIdUseCase : IGetPersonByIdUseCase
{
    private readonly IPersonRepository _repository;

    public GetPersonByIdUseCase(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Person>> ExecuteAsync(long id)
    {
        var person = await _repository.GetByIdAsync(id);
        if (person == null)
            return Result<Person>.Failure("Pessoa não encontrada.");

        return Result<Person>.Success(person);
    }
}