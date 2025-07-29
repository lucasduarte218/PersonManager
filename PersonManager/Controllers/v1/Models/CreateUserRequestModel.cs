namespace PersonManager.API.Controllers.v1.Models
{
    public class CreateUserRequestModel
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? Role { get; set; }
    }
}