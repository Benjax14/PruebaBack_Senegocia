using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaBack_Senegocia.Models.Entities
{
    public enum StatusCourse
    {
        Inscrito,
        NoInscrito,
        
    }
    public class Student_Course
    {
        [ForeignKey("Student")]
        public int Id_Student { get; set; }

        [ForeignKey("Course")]
        public int Id_Course { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Student_Course { get; set; }

        [Required]
        public StatusCourse Status { get; set; }
    }
}
