using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PencaUcuApi.Models;

namespace PencaUcuApi.Controllers;
// FIXME: Hacer rest decente
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