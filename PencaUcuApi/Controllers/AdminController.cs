using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using PencaUcuApi.Models;
using PencaUcuApi.DTOs;

namespace PencaUcuApi.Controllers;
[ApiController]
[Route("[controller]")]

public class AdminController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    private readonly ILogger<UserController> _logger;

    public AdminController(ILogger<UserController> logger, MyDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Get()
    {
        // TODO: Implement your logic here
        return Ok("Get method called");
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
                    + "INNER JOIN Administrators as A ON U.Id = A.AdminId AND A.AdminId = @adminId;",
                new MySqlParameter("@adminId", data.Id)
            )
            .ToListAsync();

        if (!query.Any())
        {
            return NotFound($"Admin (id= '{data.Id}') not found");
        }
        else if (query[0].Password != data.Password) {
            return NotFound($"Password incorrect. Please try again.");
        }
        return Ok(query[0]);
        
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] object data)
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
