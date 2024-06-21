using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
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
public class MatchesResultsController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    private readonly ILogger<UserController> _logger;

    public MatchesResultsController(ILogger<UserController> logger, MyDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<MatchResult>> PostMatchResult(MatchResult matchResult)
    {
        var sql = "INSERT INTO MatchResults (MatchId, LocalNationalTeamGoals, VisitorNationalTeamGoals, WinnerId) " +
                    "VALUES (@MatchId, @LocalNationalTeamGoals, @VisitorNationalTeamGoals, @WinnerId)";

        await _dbContext.Database.ExecuteSqlRawAsync(sql,
            new MySqlParameter("@MatchId", matchResult.MatchId),
            new MySqlParameter("@LocalNationalTeamGoals", matchResult.LocalNationalTeamGoals),
            new MySqlParameter("@VisitorNationalTeamGoals", matchResult.VisitorNationalTeamGoals),
            new MySqlParameter("@WinnerId", matchResult.WinnerId)
        );
        // IMPLEMENTAR CALCULO DE PUNTOS
        return Created( "Result uploaded" , matchResult );
    }
}