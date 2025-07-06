using Microsoft.AspNetCore.Mvc;
using PruebaBack_Senegocia.Data;
using PruebaBack_Senegocia.Models;
using PruebaBack_Senegocia.Models.Entities;

namespace PruebaBack_Senegocia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Students_CoursesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public Students_CoursesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult AddStudentCourse(JoinCourseByStudent joinCourseByStudent)
        {

            var student = dbContext.Students.Find(joinCourseByStudent.Id_Student);
            if (student is null)
            {
                return NotFound(new
                {
                    message = "Estudiante no encontrado"
                });
            }

            var course = dbContext.Courses.Find(joinCourseByStudent.Id_Course);
            if (course is null)
            {
                return NotFound(new
                {
                    message = "Curso no encontrado"
                });
            }

            var maxStudents = course.Max_Students;

            if (dbContext.Students_Courses.Count(sc => sc.Id_Course == joinCourseByStudent.Id_Course && sc.Status == StatusCourse.Inscrito) >= maxStudents)
            {
                return BadRequest(new
                {
                    message = "El curso ha alcanzado su capacidad máxima de estudiantes"
                });
            }

            var isEnrolled = dbContext.Students_Courses
                .FirstOrDefault(sc => sc.Id_Student == joinCourseByStudent.Id_Student && sc.Id_Course == joinCourseByStudent.Id_Course && 
                                                sc.Status == StatusCourse.Inscrito);

            if (isEnrolled != null)
            {
                return BadRequest(new
                {
                    message = "El estudiante ya está inscrito en este curso"
                });
            }

            var studentCourseEntity = new Student_Course()
            {
                Id_Student = joinCourseByStudent.Id_Student,
                Id_Course = joinCourseByStudent.Id_Course,
                Status = StatusCourse.Inscrito
            };
            dbContext.Students_Courses.Add(studentCourseEntity);
            dbContext.SaveChanges();
            return Ok(new
            {
                message = "Estudiante inscrito en el curso correctamente",
                studentCourseEntity
            });
        }

    }
}
