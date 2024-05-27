using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PencaUcuApi.DTOs;
using PencaUcuApi.Models;

namespace PencaUcuApi.Controllers;

[ApiController]
[Route("[controller]")]
/*
Endpoints:
GET
/profile
Params:     str ci
Devuelve:   200- Ok (Datos del estudiante) o 400- Bad Request

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
        var students = await _dbContext
            .Set<StudentDTO>()
            .FromSqlRaw("SELECT StudentId, Score FROM Students")
            .ToListAsync();
        return Ok(students);
    }

    // api/students/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(long id)
    {
        var result = await _dbContext
            .Set<Student>()
            .FromSqlRaw("SELECT StudentId, Score FROM Students")
            .ToListAsync();

        return new OkObjectResult(result[0].ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] object data)
    {
        // TODO: Implement your logic here
        return Ok("Post method called");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] object data)
    {
        // TODO: Implement your logic here
        return Ok($"Put method called with id: {id}");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        // TODO: Implement your logic here
        return Ok($"Delete method called with id: {id}");
    }
}
