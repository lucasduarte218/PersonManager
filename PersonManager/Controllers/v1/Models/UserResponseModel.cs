// PersonManager/Controllers/v1/Models/UserResponseModel.cs
namespace PersonManager.API.Controllers.v1.Models
{
    public class UserResponseModel
    {
        public long Id { get; set; }
        public string Username { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}