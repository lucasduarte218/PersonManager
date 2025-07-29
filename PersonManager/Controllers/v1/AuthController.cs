using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonManager.API.Controllers.v1.Models;
using PersonManager.Application.DTOs;
using PersonManager.Application.Interfaces.UseCasesInterfaces;
using PersonManager.Application.Services;
using PersonManager.Domain.Repositories;

namespace PersonManager.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ICreateUserUseCase _createUserUseCase;

    public AuthController(IUserRepository userRepository, IJwtTokenService jwtTokenService, ICreateUserUseCase createUserUseCase)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _createUserUseCase = createUserUseCase;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Usuário ou senha inválidos.");

        var token = _jwtTokenService.GenerateToken(user);

        return Ok(new LoginResponseDto
        {
            Token = token,
            Username = user.Username,
            Role = user.Role
        });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] CreateUserRequestModel model)
    {
        var dto = new CreateUserDto
        {
            Username = model.Username,
            Password = model.Password,
            Role = model.Role ?? "User"
        };

        var result = await _createUserUseCase.ExecuteAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        var response = new UserResponseModel
        {
            Id = result.Value!.Id,
            Username = result.Value.Username,
            Role = result.Value.Role
        };

        return Ok(response);
    }
}