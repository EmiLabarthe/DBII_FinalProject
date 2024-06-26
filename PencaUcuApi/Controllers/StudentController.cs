using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using PencaUcuApi.DTOs;
using PencaUcuApi.Models;

namespace PencaUcuApi.Controllers;

[ApiController]
[Route("[controller]")]
/*
GET
/career_choice
Params:     str id_career, str ci
Devuelve:   200- Ok o 400- Bad request
Eligen la carrera que cursan luego de registrarse

POST
/predict_finals
Params:     str id_champ, str id_sub, str ci
Devuelve:   200- Ok o 400- Bad request
Eligen sus ganadores del torneo

*/
public class StudentController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    private readonly ILogger<UserController> _logger;

    public StudentController(ILogger<UserController> logger, MyDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    // api/students
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentDTO>))]
    public async Task<IActionResult> Get()
    {
        var response = await _dbContext
            .Set<Student>()
            .FromSqlRaw("SELECT StudentId, Score FROM Students")
            .ToListAsync();

        var studentDtos = response.Select(student => student.ToDto()).ToList();

        return Ok(studentDtos);
    }

    // api/students/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var student = await _dbContext
            .Set<Student>()
            .FromSqlRaw(
                "SELECT StudentId, Score FROM Students WHERE StudentId = @id",
                new MySqlParameter("@id", id)
            )
            .FirstOrDefaultAsync();

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student.ToDto());
    }

    // /students
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StudentDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] StudentDTO student)
    {
        if (student == null)
        {
            return BadRequest();
        }

        int rowsAffected = await _dbContext.Database.ExecuteSqlRawAsync(
            "INSERT INTO Students (StudentId, Score) VALUES (@id, @score)",
            new MySqlParameter("@id", student.StudentId),
            new MySqlParameter("@score", student.Score)
        );

        if (rowsAffected == 0)
        {
            return NotFound();
        }

        return CreatedAtAction(
            nameof(Get),
            new { id = student.StudentId },
            new { message = "Student saved" }
        );
    }

    // /Student/login
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] LoginDTO data)
    {
        if (data == null)
        {
            return BadRequest();
        }

        var query = await _dbContext
            .UserDTOs.FromSqlRaw(
                "SELECT * FROM Users as U "
                    + "INNER JOIN Students as S ON U.Id = S.StudentId AND S.StudentId = @studentId;",
                new MySqlParameter("@studentId", data.Id)
            )
            .ToListAsync();

        if (!query.Any())
        {
            return NotFound($"Student (id= '{data.Id}') not found");
        }
        else if (query[0].Password != data.Password) {
            return NotFound($"Password incorrect. Please try again.");
        }
        return Ok(query[0]);
        
    }

    // api/students/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        int rowsAffected = await _dbContext.Database.ExecuteSqlRawAsync(
            "DELETE FROM Students WHERE StudentId = @id",
            new MySqlParameter("@id", id)
        );

        if (rowsAffected == 0)
        {
            return NotFound();
        }

        return Ok($"Student with id: {id} has been deleted");
    }
}
