using PruebaBack_Senegocia.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaBack_Senegocia.Models
{
    public class JoinCourseByStudent
    {
        [ForeignKey("Student")]
        public int Id_Student { get; set; }

        [ForeignKey("Course")]
        public int Id_Course { get; set; }

    }
}
