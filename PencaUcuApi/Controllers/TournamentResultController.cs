using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using PencaUcuApi.DTOs;
using PencaUcuApi.Models;

namespace PencaUcuApi.Controllers;
[ApiController]
[Route("[controller]")]

public class TournamentResultController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    private readonly ILogger<UserController> _logger;

    public TournamentResultController(ILogger<UserController> logger, MyDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpPut]
    public async Task<ActionResult<TournamentResultDTO>> PostTournamentResult(TournamentResultDTO res)
    {
        // CALCULATE STUDENTS NEW POINTS
        await updatePointsAsync(res);

        return Ok(res);
    }

    private async Task updatePointsAsync(TournamentResultDTO res)
    {
        var championQuery = await _dbContext
            .StudentTournamentPredictions
            .FromSqlRaw(
                $"SELECT * FROM StudentTournamentPrediction as P WHERE P.ChampionId = @id;",
                new MySqlParameter("@id", res.ChampionId)
            )
            .ToListAsync();
        foreach(StudentTournamentPrediction item in championQuery){
            await _dbContext.Database.ExecuteSqlRawAsync(
                "UPDATE Students SET Score = Score + 10 WHERE StudentId = @id;", 
                new MySqlParameter("@id", item.StudentId)
            );
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("estoy");
        }
        var viceChampionQuery = await _dbContext
            .StudentTournamentPredictions
            .FromSqlRaw(
                $"SELECT * FROM StudentTournamentPrediction as P WHERE P.ViceChampionId = @id;",
                new MySqlParameter("@id", res.ViceChampionId)
            )
            .ToListAsync();
        foreach(StudentTournamentPrediction item in viceChampionQuery){
            await _dbContext.Database.ExecuteSqlRawAsync(
                "UPDATE Students SET Score = Score + 5 WHERE StudentId = @id;", 
                new MySqlParameter("@id", item.StudentId)
            );
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("estoy");
        }
    }
}