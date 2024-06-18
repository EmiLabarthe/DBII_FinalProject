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
    public async Task<IActionResult> GetPrediction(string predictionId)
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
                res.MatchId ?? 0,
                res.LocalNationalTeamPredictedGoals ?? 0,
                res.VisitorNationalTeamPredictedGoals ?? 0
            );

            return Ok(predictionDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching prediction id ={PredictionId}", predictionId);
            return BadRequest("An error occurred while fetching the prediction");
        }
    }

    [HttpGet("items/{studentId}")] // Prediction/items/studentId
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PredictionItem[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItemsByStudentId(string studentId)
    {
        try
        {
            var currentTime = DateTime.Now;
            var query = await _dbContext
                .PredictionItemDTO.FromSqlRaw(
                    "SELECT M.LocalNationalTeam, P.LocalNationalTeamPredictedGoals, M.VisitorNationalTeam, P.VisitorNationalTeamPredictedGoals, M.Date, S.Name as StadiumName, S.State, S.City "
                        + "FROM Predictions as P "
                        + "INNER JOIN Matches as M ON P.MatchId = M.Id "
                        + "LEFT JOIN Stadiums as S ON M.StadiumId = S.Id "
                        + $"WHERE P.StudentId = @studentId and M.Date > {currentTime};",
                    new MySqlParameter("@studentId", studentId)
                )
                .ToListAsync();
            Console.WriteLine(query);

            if (!query.Any())
            {
                return NotFound("Student (id= '" + studentId + "') prediction items not found");
            }

            var predictionItems = query
                .Select(p => new PredictionItem(
                    p.LocalNationalTeam,
                    p.LocalNationalTeamPredictedGoals ?? 0,
                    p.VisitorNationalTeam,
                    p.VisitorNationalTeamPredictedGoals ?? 0,
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
                "Error fetching predictions items for student with id '" + studentId + "'."
            );
            return BadRequest(
                "An error occurred while fetching the prediction-items for student with id '"
                    + studentId
                    + "'."
            );
        }
    }

    [HttpGet("{predictionId}/item")] // Prediction/:predictionId
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PredictionItem))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItemById(string predictionId)
    {
        try
        {
            var currentTime = DateTime.Now;
            var query = await _dbContext
                .PredictionItemDTO.FromSqlRaw(
                    "SELECT M.LocalNationalTeam, P.LocalNationalTeamPredictedGoals, M.VisitorNationalTeam, P.VisitorNationalTeamPredictedGoals, M.Date, S.Name as StadiumName, S.State, S.City "
                        + "FROM Predictions as P "
                        + "INNER JOIN Matches as M ON P.MatchId = M.Id "
                        + "LEFT JOIN Stadiums as S ON M.StadiumId = S.Id "
                        + $"WHERE P.Id = @id and M.Date > {currentTime};",
                    new MySqlParameter("@id", predictionId)
                )
                .ToListAsync();
            Console.WriteLine(query);

            if (!query.Any())
            {
                return NotFound("Prediction item not found");
            }

            var predictionItemDTO = query.First();
            var predictionItem = new PredictionItem(
                predictionItemDTO.LocalNationalTeam,
                predictionItemDTO.LocalNationalTeamPredictedGoals ?? 0,
                predictionItemDTO.VisitorNationalTeam,
                predictionItemDTO.VisitorNationalTeamPredictedGoals ?? 0,
                predictionItemDTO.Date,
                predictionItemDTO.StadiumName,
                predictionItemDTO.State,
                predictionItemDTO.City
            );

            return Ok(predictionItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error fetching prediction data for id {PredictionId}",
                predictionId
            );
            return BadRequest("An error occurred while fetching the prediction data");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PredictionDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] PredictionDTO data)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Database.ExecuteSqlInterpolated(
                $"INSERT INTO Predictions(StudentId, MatchId, LocalNationalTeamPredictedGoals, VisitorNationalTeamPredictedGoals) VALUES ({data.StudentId},{data.MatchId},{data.LocalNationalTeamPredictedGoals},{data.VisitorNationalTeamPredictedGoals});"
            );
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetPrediction),
                new { id = data.StudentId },
                new
                {
                    message = $"Prediction of student '{data.StudentId}, for match '{data.MatchId}' has been uploaded."
                }
            );
        }

        return BadRequest(ModelState);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] object data)
    {
        // TODO: Implement your logic here
        return Ok($"Put method called with id: {id}");
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

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        // TODO: Implement your logic here
        return Ok($"Delete method called with id: {id}");
    }
}
