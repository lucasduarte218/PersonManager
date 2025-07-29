using PersonManager.Application.Interfaces.UseCasesInterfaces;
using PersonManager.Domain.Entities;
using PersonManager.Domain.Repositories;

namespace PersonManager.Application.UseCases;

public class DeletePersonUseCase : IDeletePersonUseCase
{
    private readonly IPersonRepository _repository;

    public DeletePersonUseCase(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> ExecuteAsync(long id)
    {
        var person = await _repository.GetByIdAsync(id);
        if (person == null)
            return Result.Failure("Pessoa não encontrada.");

        await _repository.DeleteAsync(person);

        return Result.Success();
    }
}