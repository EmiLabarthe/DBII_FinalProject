using Microsoft.AspNetCore.Http.Features;
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
        // INSERT NEW RESULT
        var sql = "INSERT INTO MatchResults (MatchId, LocalNationalTeamGoals, VisitorNationalTeamGoals, WinnerId) " +
                    "VALUES (@MatchId, @LocalNationalTeamGoals, @VisitorNationalTeamGoals, @WinnerId)";

        await _dbContext.Database.ExecuteSqlRawAsync(sql,
            new MySqlParameter("@MatchId", matchResult.MatchId),
            new MySqlParameter("@LocalNationalTeamGoals", matchResult.LocalNationalTeamGoals),
            new MySqlParameter("@VisitorNationalTeamGoals", matchResult.VisitorNationalTeamGoals),
            new MySqlParameter("@WinnerId", matchResult.WinnerId)
        );
        // CALCULATE STUDENTS NEW POINTS
        await updatePointsAsync(matchResult);

        return Created( "Result uploaded" , matchResult );
    }

    private async Task updatePointsAsync(MatchResult matchResult)
    {
        // EMPATE
        if(matchResult.LocalNationalTeamGoals == matchResult.VisitorNationalTeamGoals){
            var tieQuery = await _dbContext
                .PredictionDTO
                .FromSqlRaw(
                    $"SELECT * FROM Predictions as P WHERE P.MatchId = @id AND P.LocalNationalTeamPredictedGoals = P.VisitorNationalTeamPredictedGoals;",
                    new MySqlParameter("@id", matchResult.MatchId)
                )
                .ToListAsync();
            foreach(PredictionDTO item in tieQuery){
                if(item.LocalNationalTeamPredictedGoals == matchResult.LocalNationalTeamGoals){
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        "UPDATE Students SET Score = Score + 4 WHERE StudentId = @id;", 
                        new MySqlParameter("@id", item.StudentId)
                    );
                    await _dbContext.SaveChangesAsync();
                    Console.WriteLine("estoy");
                }else{
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        "UPDATE Students SET Score = Score + 2 WHERE StudentId = @id;", 
                        new MySqlParameter("@id", item.StudentId)
                    );
                    await _dbContext.SaveChangesAsync();
                    Console.WriteLine("estoy");
                }
            }
        }

        // GANA EL LOCAL
        if(matchResult.LocalNationalTeamGoals > matchResult.VisitorNationalTeamGoals)
        {
            var LocalWinQuery = await _dbContext
                .PredictionDTO
                .FromSqlRaw(
                    $"SELECT * FROM Predictions as P WHERE P.MatchId = @id AND P.LocalNationalTeamPredictedGoals > P.VisitorNationalTeamPredictedGoals;",
                    new MySqlParameter("@id", matchResult.MatchId)
                )
                .ToListAsync();
            foreach(PredictionDTO item in LocalWinQuery){
                if(item.LocalNationalTeamPredictedGoals == matchResult.LocalNationalTeamGoals && item.VisitorNationalTeamPredictedGoals == matchResult.VisitorNationalTeamGoals){
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        "UPDATE Students SET Score = Score + 4 WHERE StudentId = @id;", 
                        new MySqlParameter("@id", item.StudentId)
                    );
                    await _dbContext.SaveChangesAsync();
                    Console.WriteLine("estoy");
                }else{
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        "UPDATE Students SET Score = Score + 2 WHERE StudentId = @id;", 
                        new MySqlParameter("@id", item.StudentId)
                    );
                    await _dbContext.SaveChangesAsync();
                    Console.WriteLine("estoy");
                }
            }
        }

        // GANA EL VISITANTE
        if(matchResult.LocalNationalTeamGoals < matchResult.VisitorNationalTeamGoals){
            var VisitorWinQuery = await _dbContext
                .PredictionDTO
                .FromSqlRaw(
                    $"SELECT * FROM Predictions as P WHERE P.MatchId = @id AND P.LocalNationalTeamPredictedGoals < P.VisitorNationalTeamPredictedGoals;",
                    new MySqlParameter("@id", matchResult.MatchId)
                )
                .ToListAsync();
            foreach(PredictionDTO item in VisitorWinQuery){
                if(item.LocalNationalTeamPredictedGoals == matchResult.LocalNationalTeamGoals && item.VisitorNationalTeamPredictedGoals == matchResult.VisitorNationalTeamGoals){
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        "UPDATE Students SET Score = Score + 4 WHERE StudentId = @id;",
                        new MySqlParameter("@id", item.StudentId)
                    );
                    await _dbContext.SaveChangesAsync();
                    Console.WriteLine("estoy");
                }else{
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        "UPDATE Students SET Score = Score + 2 WHERE StudentId = @id;", 
                        new MySqlParameter("@id", item.StudentId)
                    );
                    await _dbContext.SaveChangesAsync();
                    Console.WriteLine("estoy");
                }
            }
        }
    }
}