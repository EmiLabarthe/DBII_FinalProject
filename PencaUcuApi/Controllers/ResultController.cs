using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using PencaUcuApi.DTOs;
using PencaUcuApi.Models;

namespace PencaUcuApi.Controllers;

[ApiController]
[Route("[controller]")]
/*
*/
public class ResultController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    private readonly ILogger<UserController> _logger;

    public ResultController(ILogger<UserController> logger, MyDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("{id}")] // Result/:id
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MatchResultDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(long id)
    {
        try
        {
            var currentTime = DateTime.Now;
            var query = await _dbContext
                .MatchResultsDTO.FromSqlRaw(
                    $"SELECT * FROM MatchResults as R WHERE R.Id = @id;",
                    new MySqlParameter("@id", id)
                )
                .ToListAsync();
            Console.WriteLine(query);

            if (!query.Any())
            {
                return NotFound("MatchResult not found");
            }

            var res = query.First();
            var MatchResultDto = new MatchResultDTO(
                res.MatchId,
                res.LocalNationalTeamGoals,
                res.VisitorNationalTeamGoals,
                res.WinnerId
            );
            return Ok(MatchResultDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching the result {id}", id);
            return BadRequest("An error occurred while fetching the result");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MatchResultDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] MatchResultDTO data)
    {
        if (ModelState.IsValid)
        {
            var sql =
                "INSERT INTO MatchResults (MatchId, LocalNationalTeamGoals, VisitorNationalTeamGoals, WinnerId) "
                + "VALUES (@matchId, @localGoals, @visitorGoals, @winnerId)";

            await _dbContext.Database.ExecuteSqlRawAsync(
                sql,
                new MySqlParameter("@matchId", data.MatchId),
                new MySqlParameter("@localGoals", data.LocalNationalTeamGoals),
                new MySqlParameter("@visitorGoals", data.VisitorNationalTeamGoals),
                new MySqlParameter("@studentId", data.WinnerId)
            );
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(Get),
                new { id = data.Id },
                new { message = $"Result for match '{data.MatchId}' has been uploaded." }
            );
        }

        return BadRequest(ModelState);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MatchResultDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromBody] MatchResultDTO data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await Get(data.Id);
        if (result == null)
        {
            return NotFound($"Match result with id '{data.Id}' not found.");
        }

        try
        {
            var sql =
                "UPDATE MatchResults "
                + "SET LocalNationalTeamGoals = @localGoals, "
                + "VisitorNationalTeamGoals = @visitorGoals, "
                + "WinnerId = @winnerId "
                + "WHERE Id = @id";

            var rowsAffected = await _dbContext.Database.ExecuteSqlRawAsync(
                sql,
                new MySqlParameter("@id", data.Id),
                new MySqlParameter("@localGoals", data.LocalNationalTeamGoals),
                new MySqlParameter("@visitorGoals", data.VisitorNationalTeamGoals),
                new MySqlParameter("@winnerId", data.WinnerId)
            );

            if (rowsAffected == 0)
            {
                return NotFound($"Match result with id '{data.Id}' not found.");
            }

            await _dbContext.SaveChangesAsync();
            return new OkObjectResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating match-result with id '{data.Id}'.", data.Id);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "An error occurred while updating the match result."
            );
        }
    }

    [HttpGet("{studentId}-prediction")] // Result/:studentId-prediction
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PredictionResultItem[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItemsByStudentId(string studentId)
    {
        try
        {
            var query = await _dbContext
                .PredictionResultItemDTO.FromSqlRaw(
                    "SELECT M.LocalNationalTeam, R.LocalNationalTeamGoals, P.LocalNationalTeamPredictedGoals, M.VisitorNationalTeam, R.VisitorNationalTeamGoals, P.VisitorNationalTeamPredictedGoals, M.Date, S.Name as StadiumName, S.State, S.City "
                        + "FROM Matches as M "
                        + "INNER JOIN MatchResults as R ON M.Id = R.MatchId "
                        + "INNER JOIN Predictions as P ON P.MatchId = M.Id "
                        + "LEFT JOIN Stadiums as S ON M.StadiumId = S.Id "
                        + "WHERE P.StudentId = @studentId;",
                    new MySqlParameter("@studentId", studentId)
                )
                .ToListAsync();

            if (!query.Any())
            {
                return NotFound($"Student (id= '{studentId}') prediction-result items not found");
            }

            var predictionResultItems = query
                .Select(p =>
                {
                    var points = GetPoints(
                        p.LocalNationalTeamGoals,
                        p.LocalNationalTeamPredictedGoals,
                        p.VisitorNationalTeamGoals,
                        p.VisitorNationalTeamPredictedGoals
                    );
                    return new PredictionResultItem(
                        p.LocalNationalTeam,
                        p.LocalNationalTeamGoals,
                        p.LocalNationalTeamPredictedGoals,
                        p.VisitorNationalTeam,
                        p.VisitorNationalTeamGoals,
                        p.VisitorNationalTeamPredictedGoals,
                        points,
                        p.Date,
                        p.StadiumName,
                        p.State,
                        p.City
                    );
                })
                .ToList();

            return Ok(predictionResultItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error fetching prediction-result items for student with id '{StudentId}'.",
                studentId
            );
            return BadRequest(
                $"An error occurred while fetching the prediction-result items for student with id '{studentId}'."
            );
        }
    }

    [HttpGet("{matchId}/item")] // Result/:matchtId/item
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PredictionResultItem))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItemById(string matchId)
    {
        try
        {
            var query = await _dbContext
                .PredictionResultItemDTO.FromSqlRaw(
                    "SELECT M.LocalNationalTeam, R.LocalNationalTeamGoals, P.LocalNationalTeamPredictedGoals, M.VisitorNationalTeam, R.VisitorNationalTeamGoals, P.VisitorNationalTeamPredictedGoals, M.Date, S.Name as StadiumName, S.State, S.City "
                        + "FROM Matches as M "
                        + "INNER JOIN MatchResults as R ON M.Id = R.MatchId "
                        + "INNER JOIN Predictions as P ON P.MatchId = M.Id "
                        + "LEFT JOIN Stadiums as S ON M.StadiumId = S.Id "
                        + "WHERE M.Id = @id;",
                    new MySqlParameter("@id", matchId)
                )
                .ToListAsync();
            Console.WriteLine(query);

            if (!query.Any())
            {
                return NotFound("prediction-result item not found");
            }

            var predictionPredictionResultItemDTO = query.First();
            var predictionResultItem = new PredictionResultItem(
                predictionPredictionResultItemDTO.LocalNationalTeam,
                predictionPredictionResultItemDTO.LocalNationalTeamGoals,
                predictionPredictionResultItemDTO.LocalNationalTeamPredictedGoals,
                predictionPredictionResultItemDTO.VisitorNationalTeam,
                predictionPredictionResultItemDTO.VisitorNationalTeamGoals,
                predictionPredictionResultItemDTO.VisitorNationalTeamPredictedGoals,
                predictionPredictionResultItemDTO.Points,
                predictionPredictionResultItemDTO.Date,
                predictionPredictionResultItemDTO.StadiumName,
                predictionPredictionResultItemDTO.State,
                predictionPredictionResultItemDTO.City
            );

            return Ok(predictionResultItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                $"Error fetching prediction-result data for match with id= '{matchId}'",
                matchId
            );
            return BadRequest("An error occurred while fetching the prediction-result data");
        }
    }

    private int GetPoints(
        int localGoals,
        int localPredictedGoals,
        int visitorGoals,
        int visitorPredictedGoals
    )
    {
        if (localGoals == localPredictedGoals && visitorGoals == visitorPredictedGoals) // predicted the exact result correctly
        {
            return 4;
        }
        else if ( // predicted the winner correctly
            (localGoals > visitorGoals && localPredictedGoals > visitorPredictedGoals)
            || (localGoals < visitorGoals && localPredictedGoals < visitorPredictedGoals)
        )
        {
            return 2;
        }
        return 0;
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        // TODO: Implement your logic here
        return Ok($"Delete method called with id: {id}");
    }
}
