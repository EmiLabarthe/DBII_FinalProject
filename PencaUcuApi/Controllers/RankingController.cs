using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using PencaUcuApi.DTOs;

namespace PencaUcuApi.Controllers;
[ApiController]
[Route("[controller]")]
/*
Endpoints:
GET
/get_scores	
Params:     -
Devuelve:   200- Ok (Nombre y puntaje) o 400- Bad request	
Lista de todos los puntajes con el nombre de la persona
select FirstName, LastName, Score from Students s inner join Users u on s.StudentId = u.Id order by Score DESC;
*/
public class RankingController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    private readonly ILogger<UserController> _logger;

    public RankingController(ILogger<UserController> logger, MyDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("Scores")]
    public IActionResult GetScores()
    {
        var users = _dbContext.Set<StudentWithUserDTO>().FromSqlRaw("select FirstName, LastName, Score from Students s inner join Users u on s.StudentId = u.Id order by Score DESC;").ToList();
        return Ok(users);
    }
    /*
        [HttpPost]
        public IActionResult Post([FromBody] object data)
        {
            // TODO: Implement your logic here
            return Ok("Post method called");
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
    */
}
