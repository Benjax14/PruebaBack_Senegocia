using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaBack_Senegocia.Models.Entities
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Student { get; set; }
        public required string Name { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
