using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using PencaUcuApi.DTOs;
using PencaUcuApi.Models;

namespace PencaUcuApi.Controllers;
// TODO: 
/*

Endpoints:
POST
/register	
Params:     los de user	
Devuelve:   200- Ok(User) o 409- Conflict  	
            Solo para estudiantes, debe crear el estudiante tmb. 

POST
/login	
Params:     str psw, str mail	
Devuelve:   200- Ok o 401-Unauthorized 

En ambos hay que hashear la contraseña 		
*/
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    private readonly ILogger<UserController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public UserController(ILogger<UserController> logger, MyDbContext dbContext, IHttpClientFactory httpClient)
    {
        _httpClientFactory = httpClient;
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Get()
    {
        // Implement your GET logic here
        var users = _dbContext.Users.ToList();
        return Ok(users);
    }

    [HttpGet("Prueba")]
    public IActionResult Prueba()
    {
        return Ok("Bien");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        if(ModelState.IsValid){

            _dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Users (Id, FirstName, LastName, Gender, Email, Password) VALUES ({user.Id}, {user.FirstName}, {user.LastName}, {user.Gender}, {user.Email}, {user.Password})");
            await _dbContext.SaveChangesAsync();

            var student = new StudentDTO(user.Id, 0);
            /*
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var result = await httpClient.PostAsJsonAsync("http://localhost:8080/Student", student);
                if (!result.IsSuccessStatusCode)
                {
                    _logger.LogError("Error al llamar al controlador Student: {StatusCode}", result.StatusCode);
                    return StatusCode((int)result.StatusCode, "Error al llamar al controlador Student.");
                }
            }*/
            var result = await PostStudent(student);
            if (result is ObjectResult objectResult && objectResult.StatusCode >= 200 && objectResult.StatusCode <= 299)
        {
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new { message = "Usuario creado con éxito" });
        }
        else
        {
            _logger.LogError("Error al llamar al controlador Student: {StatusCode}", result);
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error al llamar al controlador Student.");
        }
        }



        return BadRequest(ModelState);
    }

    [HttpPost("student")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StudentDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PostStudent([FromBody] StudentDTO student)
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

        return CreatedAtAction(nameof(Get), new { id = student.StudentId }, new { message = "Student saved" });
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _dbContext.Users.FromSqlRaw("SELECT * FROM Users WHERE Id = {0}", id)
        .FirstOrDefault();
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // api/users/
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromBody] UserDTO user)
    {
        User? entity = await _dbContext
            .Set<User>()
            .FromSqlRaw(
                "SELECT * FROM Users WHERE Id = @id",
                new MySqlParameter("@id", user.Id)
            )
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            return NotFound();
        }

        entity.FirstName = user.FirstName;
        entity.LastName = user.LastName;
        entity.Email = user.Email;
        entity.Gender = user.Gender;

        _dbContext.Update<User>(entity);
        await _dbContext.SaveChangesAsync();
        return new OkObjectResult(entity.ToDto());
    }

}