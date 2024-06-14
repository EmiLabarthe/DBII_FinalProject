using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PencaUcuApi.DTOs;
using PencaUcuApi.Models;

namespace PencaUcuApi.Controllers;
[ApiController]
[Route("[controller]")]
/*
Endpoints:
/get_fixture	
Params:     -	
Devuelve:   200- Ok ((Seleccion, grupo) y (Partidos, fase)) o 400- Bad request	
Para armar grupos y partidos de fase a medida que se va avanzando
*/
public class FixtureController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    private readonly ILogger<UserController> _logger;

    public FixtureController(ILogger<UserController> logger, MyDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
            try
            {
                var query = await _dbContext.FixtureItemDTO
                    .FromSqlRaw("SELECT M.LocalNationalTeam, M.VisitorNationalTeam, M.Date, S.Name as StadiumName, S.State, S.City FROM Matches as M LEFT JOIN Stadiums as S ON M.StadiumId = S.Id")
                    .ToListAsync();
                    
                var fixtureItems = query.Select(m => new FixtureItem(
                    m.LocalNationalTeam ?? throw new ArgumentNullException(nameof(m.LocalNationalTeam)),
                    m.VisitorNationalTeam ?? throw new ArgumentNullException(nameof(m.VisitorNationalTeam)),
                    m.Date,
                    m.StadiumName ?? throw new ArgumentNullException(nameof(m.StadiumName)),
                    m.State ?? throw new ArgumentNullException(nameof(m.State)),
                    m.City ?? throw new ArgumentNullException(nameof(m.City))
                )).ToList();

                return Ok(fixtureItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching fixture data");
                return BadRequest("An error occurred while fetching fixture data");
            }
        }

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
}
