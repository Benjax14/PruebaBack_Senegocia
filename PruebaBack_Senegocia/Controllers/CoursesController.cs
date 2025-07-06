using Microsoft.AspNetCore.Mvc;
using PruebaBack_Senegocia.Data;
using PruebaBack_Senegocia.Models;
using PruebaBack_Senegocia.Models.Entities;

namespace PruebaBack_Senegocia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public CoursesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    

        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var allCourses = dbContext.Courses.ToList();
            return Ok(allCourses);
        }

        [HttpPost]
        public IActionResult AddCourse(AddCourseDTO addCourseDTO)
        {

            var existingCourse = dbContext.Courses.FirstOrDefault(c => c.Name == addCourseDTO.Name);
            if (existingCourse != null)
            {
                return BadRequest(new
                {
                    message = "Ya existe un curso con ese nombre"
                });
            }

            if (addCourseDTO.Max_Students <= 0)
            {
                return BadRequest(new
                {
                    message = "El número máximo de estudiantes debe ser mayor que cero"
                });
            }

            var courseEntity = new Course()
            {
                Name = addCourseDTO.Name,
                Max_Students = addCourseDTO.Max_Students
            };

            dbContext.Courses.Add(courseEntity);
            dbContext.SaveChanges();

            return Ok(new
            {
                message = "Curso agregado correctamente",
                courseEntity
            });
        }

        [HttpPut]
        [Route("{Id_Course}")]
        public IActionResult UpdateCourse(int Id_Course, UpdateCourseDTO updateCourseDTO)
        {
            var course = dbContext.Courses.Find(Id_Course);
            if (course is null)
            {
                return NotFound();
            }

            var existingCourse = dbContext.Courses.FirstOrDefault(c => c.Name == updateCourseDTO.Name && c.Id_Course != Id_Course);
            if (existingCourse != null)
            {
                return BadRequest(new
                {
                    message = "Ya existe un curso con ese nombre"
                });
            }

            if (updateCourseDTO.Max_Students <= 0)
            {
                return BadRequest(new
                {
                    message = "El número máximo de estudiantes debe ser mayor que cero"
                });
            }

            course.Name = updateCourseDTO.Name;

            course.Max_Students = updateCourseDTO.Max_Students;

            dbContext.SaveChanges();
            return Ok(new
            {
                message = "Curso actualizado correctamente",
                course
            });
        }

        [HttpDelete]
        [Route("{Id_Course}")]
        public IActionResult DeleteCourse(int Id_Course)
        {
            var course = dbContext.Courses.Find(Id_Course);

            if (course is null)
            {
                return NotFound();
            }

            dbContext.Courses.Remove(course);
            dbContext.SaveChanges();
            return Ok(new
            {
                message = "Curso eliminado correctamente",
                course
            });
        }

    }
}
