using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public UserController(ILogger<UserController> logger, MyDbContext dbContext)
    {
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
    public IActionResult Post([FromBody] User user)
    {
        // Implement your POST logic here
        return Ok("Funcionó y ahora?");
    }


}