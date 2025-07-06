using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaBack_Senegocia.Models
{
    public class AddResultDTO
    {
        [Required]
        [ForeignKey("Student")]
        public int Id_Student { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int Id_Course { get; set; }

        [Required]
        public decimal Score { get; set; }
        public DateOnly Date { get; set; }
        [Required]
        public string Observation { get; set; }
    }
}
