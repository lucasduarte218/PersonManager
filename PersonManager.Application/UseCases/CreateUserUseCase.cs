using PersonManager.Application.DTOs;
using PersonManager.Application.Interfaces.UseCasesInterfaces;
using PersonManager.Domain.Entities;
using PersonManager.Domain.Repositories;

namespace PersonManager.Application.UseCases;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserRepository _userRepository;

    public CreateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<User>> ExecuteAsync(CreateUserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Username))
            return Result<User>.Failure("Username é obrigatório.");

        if (string.IsNullOrWhiteSpace(dto.Password))
            return Result<User>.Failure("Senha é obrigatória.");

        var existing = await _userRepository.GetByUsernameAsync(dto.Username);
        if (existing != null)
            return Result<User>.Failure("Usuário já existe.");

        var user = new User
        {
            Username = dto.Username,

            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),

            Role = dto.Role ?? "User"
        };

        await _userRepository.AddAsync(user);

        return Result<User>.Success(user);
    }
}