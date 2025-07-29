using PersonManager.Application.DTOs;
using PersonManager.Domain.Entities;

namespace PersonManager.Application.Interfaces.UseCasesInterfaces
{
    public interface IUpdatePersonUseCase
    {
        Task<Result<Person>> ExecuteAsync(UpdatePersonDto dto);
    }
}