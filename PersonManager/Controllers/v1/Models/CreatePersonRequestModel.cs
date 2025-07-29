namespace PersonManager.API.Controllers.v1.Models
{
    public class CreatePersonRequestModel
    {
        public string Name { get; set; } = default!;
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Nationality { get; set; }
        public string CPF { get; set; } = default!;
        public string? Address { get; set; } // Opcional na v1
    }
}