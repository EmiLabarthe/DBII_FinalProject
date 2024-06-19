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

    [HttpGet("{resultId}")] // Result/:resultId
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MatchResultDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetResult(string resultId)
    {
        try
        {
            var currentTime = DateTime.Now;
            var query = await _dbContext
                .MatchResultDTO.FromSqlRaw(
                    $"SELECT * FROM MatchResults as P WHERE P.Id = @id;",
                    new MySqlParameter("@id", resultId)
                )
                .ToListAsync();
            Console.WriteLine(query);

            if (!query.Any())
            {
                return NotFound("Result item not found");
            }

            var res = query.First();
            var MatchResultDto = new MatchResultDTO(
                res.StudentId,
                res.MatchId,
                res.LocalNationalTeamPredictedGoals,
                res.VisitorNationalTeamPredictedGoals
            );
            Console.WriteLine("matchId: ", MatchResultDto.MatchId);
            return Ok(MatchResultDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching Result id ={ResultId}", resultId);
            return BadRequest("An error occurred while fetching the Result");
        }
    }

    [HttpGet("items/{studentId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PredictionPredictionResultItem[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItemsByStudentId(string studentId)
    {
        try
        {
            var currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var query = await _dbContext
                .PredictionPredictionResultItemDTO.FromSqlRaw(
                    "SELECT M.LocalNationalTeam, P.LocalNationalTeamPredictedGoals, M.VisitorNationalTeam, P.VisitorNationalTeamPredictedGoals, M.Date, S.Name as StadiumName, S.State, S.City "
                        + "FROM Results as P "
                        + "INNER JOIN Matches as M ON P.MatchId = M.Id "
                        + "LEFT JOIN Stadiums as S ON M.StadiumId = S.Id "
                        + "WHERE P.StudentId = @studentId AND M.Date > @currentTime",
                    new MySqlParameter("@studentId", studentId),
                    new MySqlParameter("@currentTime", currentTime)
                )
                .ToListAsync();

            if (!query.Any())
            {
                return NotFound($"Student (id= '{studentId}') Result items not found");
            }

            var predictionResultItems = query
                .Select(p => new PredictionResultItem(
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

            return Ok(predictionResultItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error fetching Results items for student with id '{StudentId}'.",
                studentId
            );
            return BadRequest(
                $"An error occurred while fetching the Result-items for student with id '{studentId}'."
            );
        }
    }

    [HttpGet("{ResultId}/item")] // Result/:ResultId
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PredictionResultItem))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItemById(string resultId)
    {
        try
        {
            var currentTime = DateTime.Now;
            var query = await _dbContext
                .PredictionResultItemDTO.FromSqlRaw(
                    "SELECT M.LocalNationalTeam, P.LocalNationalTeamPredictedGoals, M.VisitorNationalTeam, P.VisitorNationalTeamPredictedGoals, M.Date, S.Name as StadiumName, S.State, S.City "
                        + "FROM Results as P "
                        + "INNER JOIN Matches as M ON P.MatchId = M.Id "
                        + "LEFT JOIN Stadiums as S ON M.StadiumId = S.Id "
                        + $"WHERE P.Id = @id and M.Date > {currentTime};",
                    new MySqlParameter("@id", resultId)
                )
                .ToListAsync();
            Console.WriteLine(query);

            if (!query.Any())
            {
                return NotFound("Result item not found");
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

            return Ok(PredictionResultItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error fetching Result data for id {ResultId}",
                resultId
            );
            return BadRequest("An error occurred while fetching the Result data");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResultDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] ResultDTO data)
    {
        if (ModelState.IsValid)
        {
            var sql =
                "INSERT INTO Results (StudentId, MatchId, LocalNationalTeamPredictedGoals, VisitorNationalTeamPredictedGoals) "
                + "VALUES (@studentId, @matchId, @localGoals, @visitorGoals)";

            await _dbContext.Database.ExecuteSqlRawAsync(
                sql,
                new MySqlParameter("@studentId", data.StudentId),
                new MySqlParameter("@matchId", data.MatchId),
                new MySqlParameter("@localGoals", data.LocalNationalTeamPredictedGoals),
                new MySqlParameter("@visitorGoals", data.VisitorNationalTeamPredictedGoals)
            );
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetResult),
                new { id = data.StudentId },
                new
                {
                    message = $"Result of student '{data.StudentId}, for match '{data.MatchId}' has been uploaded."
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

    // TOURNAMENT ResultS
    [HttpGet("tournament/{id}")]
    public IActionResult GetTournamentResultByStudentId(string id)
    {
        var Result = _dbContext
            .StudentTournamentResults.FromSqlRaw(
                "SELECT * FROM StudentTournamentResult WHERE StudentId = @id",
                id
            )
            .FirstOrDefault();
        if (Result == null)
        {
            return NotFound();
        }
        return Ok(Result);
    }

    [HttpPost("tournament")]
    public async Task<IActionResult> PostTournamentResult(
        [FromBody] StudentTournamentResult data
    )
    {
        if (ModelState.IsValid)
        {
            _dbContext.Database.ExecuteSqlInterpolated(
                $"INSERT INTO StudentTournamentResult(StudentId, ChampionId, ViceChampionId) VALUES ({data.StudentId},{data.ChampionId},{data.ViceChampionId});"
            );
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetTournamentResultByStudentId),
                new { id = data.StudentId },
                new
                {
                    message = $"Result {data.ChampionId}, {data.ViceChampionId} has been uploaded."
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
