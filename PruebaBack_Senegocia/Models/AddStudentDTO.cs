namespace PruebaBack_Senegocia.Models
{
    public class AddStudentDTO
    {
        public required string Name { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
