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
public class MatchesController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    private readonly ILogger<UserController> _logger;

    public MatchesController(ILogger<UserController> logger, MyDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var matches = await _dbContext.Matches
                .FromSqlRaw("SELECT * FROM Matches")
                .ToListAsync();

            return Ok(matches);
    }
}