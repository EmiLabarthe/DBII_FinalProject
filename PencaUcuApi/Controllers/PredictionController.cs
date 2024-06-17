using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
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

    [HttpGet("items/{studentId}")] // prediction/items/studentId
    public async Task<IActionResult> GetItemsByStudentId(string studentId)
    {
        Console.WriteLine("Buenas1");
        try
        {
            Console.WriteLine("Buenas2");
            var query = await _dbContext
                .PredictionItemDTO.FromSqlRaw(
                    "SELECT M.LocalNationalTeam, P.LocalNationalTeamPredictedGoals, M.VisitorNationalTeam, P.VisitorNationalTeamPredictedGoals, M.Date, S.Name as StadiumName, S.State, S.City "
                        + "FROM Predictions as P "
                        + "INNER JOIN Matches as M ON P.MatchId = M.Id "
                        + "LEFT JOIN Stadiums as S ON M.StadiumId = S.Id "
                        + "WHERE P.StudentId = @studentId AND M.Date;",
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

    [HttpGet("{id}")] // predictionId
    public async Task<IActionResult> GetById(string id)
    {
        var today = DateTime.Now;
        try
        {
            var query = await _dbContext
                .PredictionItemDTO.FromSqlRaw(
                    "SELECT M.LocalNationalTeam, P.LocalNationalTeamPredictedGoals, M.VisitorNationalTeam, P.VisitorNationalTeamPredictedGoals, M.Date, S.Name as StadiumName, S.State, S.City "
                        + "FROM Predictions as P "
                        + "INNER JOIN Matches as M ON P.MatchId = M.Id "
                        + "LEFT JOIN Stadiums as S ON M.StadiumId = S.Id "
                        + $"WHERE P.Id = @id and M.Date > {today};",
                    new MySqlParameter("@id", id)
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
            _logger.LogError(ex, "Error fetching prediction data for id {PredictionId}", id);
            return BadRequest("An error occurred while fetching the prediction data");
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
