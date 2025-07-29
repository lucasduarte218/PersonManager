using PersonManager.Application.DTOs;
using PersonManager.Domain.Entities;

namespace PersonManager.Application.Interfaces.UseCasesInterfaces;

public interface ICreateUserUseCase
{
    Task<Result<User>> ExecuteAsync(CreateUserDto dto);
}