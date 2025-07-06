using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PruebaBack_Senegocia.Data;
using PruebaBack_Senegocia.Models;
using PruebaBack_Senegocia.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace PruebaBack_Senegocia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var allStudents = dbContext.Students.ToList();

            return Ok(allStudents);
        }

        [HttpPost]
        public IActionResult AddStudent(AddStudentDTO addStudentDTO)
        {
            try {
                var existingStudent = dbContext.Students.FirstOrDefault(s => s.Email == addStudentDTO.Email);

                if (existingStudent != null)
                {
                    return BadRequest(new
                    {
                        message = "Ya existe un estudiante con ese correo electrónico"
                    });
                }

                var studentEntity = new Student()
                {
                    Name = addStudentDTO.Name,
                    Email = addStudentDTO.Email ?? string.Empty
                };

                dbContext.Students.Add(studentEntity);
                dbContext.SaveChanges();

                return Ok(new
                {
                    message = "Estudiante actualizado correctamente",
                    studentEntity
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error al agregar el estudiante",
                    error = ex.Message
                });
            }

        }

        [HttpPut]
        [Route("{Id_Student}")]
        public IActionResult UpdateStudent(int Id_Student, UpdateStudentDTO updateStudentDTO)
        {
            try
            {
                var student = dbContext.Students.Find(Id_Student);

                if (student is null)
                {
                    return NotFound();
                }

                student.Name = updateStudentDTO.Name;
                student.Email = updateStudentDTO.Email ?? string.Empty;

                dbContext.Students.Update(student);
                dbContext.SaveChanges();

                return Ok(new
                {
                    message = "Estudiante actualizado correctamente",
                    student
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error al actualizar el estudiante",
                    error = ex.Message
                });

            }
        }

        [HttpDelete]
        [Route("{Id_Student}")]
        public IActionResult DeleteStudent(int Id_Student)
        {
            var student = dbContext.Students.Find(Id_Student);

            if (student is null)
            {
                return NotFound();
            }

            dbContext.Students.Remove(student);
            dbContext.SaveChanges();
            return Ok(new { message = "Estudiante eliminado correctamente" });
        }

    }
}
