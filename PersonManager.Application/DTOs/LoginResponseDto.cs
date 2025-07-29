namespace PersonManager.Application.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}