using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PruebaBack_Senegocia.Data;
using PruebaBack_Senegocia.Models;
using PruebaBack_Senegocia.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace PruebaBack_Senegocia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ResultsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllResults()
        {
            var allResults = dbContext.Results.ToList();
            return Ok(allResults);
        }

        [HttpGet("{Id_Result}")]
        public IActionResult GetResultById(int Id_Result)
        {
            var result = dbContext.Results.FirstOrDefault(r => r.Id_Result == Id_Result);
            if (result == null)
            {
                return NotFound(new
                {
                    message = "Nota no encontrada"
                });
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddResult(AddResultDTO addResultDTO)
        {
            try
            {

                var student = dbContext.Students.Find(addResultDTO.Id_Student);
                if (student == null)
                {
                    return NotFound(new
                    {
                        message = "Estudiante no encontrado"
                    });
                }
                var course = dbContext.Courses.Find(addResultDTO.Id_Course);
                if (course == null)
                {
                    return NotFound(new
                    {
                        message = "Curso no encontrado"
                    });
                }
                var isEnrolled = dbContext.Students_Courses
                    .FirstOrDefault(sc => sc.Id_Student == addResultDTO.Id_Student && sc.Id_Course == addResultDTO.Id_Course && 
                                          sc.Status == StatusCourse.Inscrito);


                if (addResultDTO.Score < 0)
                {
                    return BadRequest(new
                    {
                        message = "La nota no puede ser negativa"
                    });
                }

                var resultEntity = new Result()
                {
                    Id_Student = addResultDTO.Id_Student,
                    Id_Course = addResultDTO.Id_Course,
                    Score = addResultDTO.Score,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Observation = addResultDTO.Observation
                };

                dbContext.Results.Add(resultEntity);
                dbContext.SaveChanges();

                return Ok(new
                {
                    message = "Nota agregada correctamente",
                    resultEntity
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error al agregar la nota",
                    error = ex.Message
                });
            }
        }

        [HttpPut("{Id_Result}")]
        public IActionResult UpdateResult(int Id_Result, UpdateResultDTO updateResultDTO)
        {
            try
            {
                var result = dbContext.Results.Find(Id_Result);
                if (result == null)
                {
                    return NotFound(new
                    {
                        message = "Nota no encontrado"
                    });
                }

                if (updateResultDTO.Score < 0)
                {
                    return BadRequest(new
                    {
                        message = "La nota no puede ser negativa"
                    });
                }

                result.Score = updateResultDTO.Score;
                result.Date = updateResultDTO.Date;
                result.Observation = updateResultDTO.Observation;

                dbContext.Results.Update(result);
                dbContext.SaveChanges();

                return Ok(new
                {
                    message = "Nota actualizada correctamente",
                    result
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error al actualizar la nota",
                    error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("averageStudent/{Id_Student}")]
        public IActionResult GetAverageByStudent(int Id_Student)
        {
            try
            {
                var results = dbContext.Results.Where(r => r.Id_Student == Id_Student).ToList();

                if (results.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "No se encontraron notas para el estudiante"
                    });
                }

                var courseAverage = dbContext.Results.Where(r => r.Id_Student == Id_Student)
                    .GroupBy(r => r.Id_Course).Select(g => new
                    {
                        Id_Course = g.Key,
                        Average = Math.Round(g.Average(r => r.Score), 2)
                    })
                    .ToList();

                return Ok(new
                {
                    Id_Student,
                    courseAverage
                });
            }
            catch
            { 
                return BadRequest(new
                {
                    message = "Error al calcular el promedio del estudiante"
                });
            }
        }

        [HttpGet]
        [Route("averageCourse/{Id_Course}")]
        public IActionResult GetAverageByCourse(int Id_Course)
        {
            try
            {
                var results = dbContext.Results.Where(r => r.Id_Course == Id_Course).ToList();
                if (results.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "No se encontraron notas para el curso"
                    });
                }

                var studentAverage = dbContext.Results.Where(r => r.Id_Course == Id_Course)
                   .GroupBy(r => r.Id_Student).Select(g => new
                   {
                       Id_Student = g.Key,
                       Average = Math.Round(g.Average(r => r.Score), 2)
                   })
                   .ToList();

                return Ok(new
                {
                    Id_Course,
                    studentAverage
                });
            }
            catch
            {
                return BadRequest(new
                {
                    message = "Error al calcular el promedio del curso"
                });
            }
        }


        [HttpGet]
        [Route("topAverage")]
        public IActionResult GetAverageAll()
        {
            try
            {
                var results = dbContext.Results.ToList();
                if (results.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "No se encontraron notas"
                    });
                }

                var top3 = results.GroupBy(r => r.Id_Student)
                    .Select(g => new
                    {
                        Id_Student = g.Key,
                        Average = Math.Round(g.Average(r => r.Score), 2)
                    })
                    .OrderByDescending(x => x.Average).Take(3).ToList();

                return Ok(new
                {
                    top3
                });
            }
            catch
            {
                return BadRequest(new
                {
                    message = "Error al calcular el top 3"
                });
            }
        }

    }
}
