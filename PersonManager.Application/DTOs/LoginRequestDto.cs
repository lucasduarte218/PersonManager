namespace PersonManager.Application.DTOs
{
    public class LoginRequestDto
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}