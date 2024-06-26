using Microsoft.AspNetCore.Http.HttpResults;
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
public class PredictionController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    private readonly ILogger<UserController> _logger;

    public PredictionController(ILogger<UserController> logger, MyDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("{predictionId}")] // Prediction/:predictionId
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PredictionDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPrediction(long predictionId)
    {
        try
        {
            var currentTime = DateTime.Now;
            var query = await _dbContext
                .PredictionDTO.FromSqlRaw(
                    $"SELECT * FROM Predictions as P WHERE P.Id = @id;",
                    new MySqlParameter("@id", predictionId)
                )
                .ToListAsync();
            Console.WriteLine(query);

            if (!query.Any())
            {
                return NotFound("Prediction item not found");
            }

            var res = query.First();
            var predictionDto = new PredictionDTO(
                res.StudentId,
                res.MatchId,
                res.LocalNationalTeamPredictedGoals,
                res.VisitorNationalTeamPredictedGoals
            );
            return Ok(predictionDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching prediction id ={PredictionId}", predictionId);
            return BadRequest("An error occurred while fetching the prediction");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PredictionDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] PredictionDTO data)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var sql =
                    "INSERT INTO Predictions (StudentId, MatchId, LocalNationalTeamPredictedGoals, VisitorNationalTeamPredictedGoals) "
                    + "VALUES (@studentId, @matchId, @localGoals, @visitorGoals);";

                await _dbContext.Database.ExecuteSqlRawAsync(
                    sql,
                    new MySqlParameter("@studentId", data.StudentId),
                    new MySqlParameter("@matchId", data.MatchId),
                    new MySqlParameter("@localGoals", data.LocalNationalTeamPredictedGoals),
                    new MySqlParameter("@visitorGoals", data.VisitorNationalTeamPredictedGoals)
                );

                var predictionDto = new PredictionDTO
                {
                    StudentId = data.StudentId,
                    MatchId = data.MatchId,
                    LocalNationalTeamPredictedGoals = data.LocalNationalTeamPredictedGoals,
                    VisitorNationalTeamPredictedGoals = data.VisitorNationalTeamPredictedGoals
                };

                return CreatedAtAction(
                    nameof(GetPrediction),
                    new { predictionId = predictionDto.Id },
                    predictionDto
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing the prediction creation.");
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error occurred while processing the request."
                );
            }
        }

        return BadRequest(ModelState);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PredictionDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromBody] PredictionDTO data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await GetPrediction(data.Id);
        if (result == null)
        {
            return NotFound($"Prediction with id '{data.Id}' not found.");
        }

        try
        {
            var sql =
                "UPDATE Predictions "
                + "SET LocalNationalTeamPredictedGoals = @localPredictedGoals, "
                + "VisitorNationalTeamPredictedGoals = @visitorPredictedGoals "
                + "WHERE Id = @id;";

            var rowsAffected = await _dbContext.Database.ExecuteSqlRawAsync(
                sql,
                new MySqlParameter("@id", data.Id),
                new MySqlParameter("@localPredictedGoals", data.LocalNationalTeamPredictedGoals),
                new MySqlParameter("@visitorPredictedGoals", data.VisitorNationalTeamPredictedGoals)
            );

            if (rowsAffected == 0)
            {
                return NotFound($"Prediction with id '{data.Id}' not found.");
            }

            await _dbContext.SaveChangesAsync();
            var updatedPredictionDTO = new PredictionDTO
            {
                Id = data.Id,
                StudentId = data.StudentId,
                MatchId = data.MatchId,
                LocalNationalTeamPredictedGoals = data.LocalNationalTeamPredictedGoals,
                VisitorNationalTeamPredictedGoals = data.VisitorNationalTeamPredictedGoals
            };

            return new ObjectResult(updatedPredictionDTO) { StatusCode = StatusCodes.Status200OK };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating prediction with id '{data.Id}'.", data.Id);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "An error occurred while updating the prediction."
            );
        }
    }

    [HttpGet("items/{studentId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PredictionItem[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItemsByStudentId(string studentId)
    {
        try
        {
            var currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var query = await _dbContext
                .PredictionItemDTO.FromSqlRaw(
                    "SELECT P.Id as PredictionId, M.Id as MatchId, M.LocalNationalTeam, P.LocalNationalTeamPredictedGoals, M.VisitorNationalTeam, P.VisitorNationalTeamPredictedGoals, M.Date, S.Name as StadiumName, S.State, S.City "
                        + "FROM Matches as M "
                        + "LEFT JOIN Predictions as P ON M.Id = P.MatchId AND P.StudentId = @studentId "
                        + "LEFT JOIN Stadiums as S ON M.StadiumId = S.Id "
                        + "WHERE M.Date > DATE_ADD(@currentTime, INTERVAL 1 HOUR)",
                    new MySqlParameter("@studentId", studentId),
                    new MySqlParameter("@currentTime", currentTime)
                )
                .ToListAsync();

            if (!query.Any())
            {
                return NotFound($"Student (id= '{studentId}') prediction items not found");
            }

            var predictionItems = query
                .Select(p => new PredictionItem(
                    p.PredictionId,
                    p.MatchId,
                    p.LocalNationalTeam,
                    p.LocalNationalTeamPredictedGoals,
                    p.VisitorNationalTeam,
                    p.VisitorNationalTeamPredictedGoals,
                    p.Date,
                    p.StadiumName,
                    p.State,
                    p.City
                ))
                .ToList();

            return Ok(predictionItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error fetching predictions items for student with id '{StudentId}'.",
                studentId
            );
            return BadRequest(
                $"An error occurred while fetching the prediction-items for student with id '{studentId}'."
            );
        }
    }

    // TOURNAMENT PREDICTIONS
    [HttpGet("tournament/{id}")]
    public IActionResult GetTournamentPredictionByStudentId(string id)
    {
        var prediction = _dbContext
            .StudentTournamentPredictions.FromSqlRaw(
                "SELECT * FROM StudentTournamentPrediction WHERE StudentId = @id",
                id
            )
            .FirstOrDefault();
        if (prediction == null)
        {
            return NotFound();
        }
        return Ok(prediction);
    }

    [HttpPost("tournament")]
    public async Task<IActionResult> PostTournamentPrediction(
        [FromBody] StudentTournamentPrediction data
    )
    {
        if (ModelState.IsValid)
        {
            _dbContext.Database.ExecuteSqlInterpolated(
                $"INSERT INTO StudentTournamentPrediction(StudentId, ChampionId, ViceChampionId) VALUES ({data.StudentId},{data.ChampionId},{data.ViceChampionId});"
            );
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetTournamentPredictionByStudentId),
                new { id = data.StudentId },
                new
                {
                    message = $"Prediction {data.ChampionId}, {data.ViceChampionId} has been uploaded."
                }
            );
        }

        return BadRequest(ModelState);
    }
}
