using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaBack_Senegocia.Models.Entities
{
    public class Course
    {
        [Key] 
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Course { get; set; }
        public required string Name { get; set; }
        public required int Max_Students { get; set; }
    }
}
