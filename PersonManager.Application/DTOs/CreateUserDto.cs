namespace PersonManager.Application.DTOs;

public class CreateUserDto
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Role { get; set; } = "User";
}