using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using PencaUcuApi.Models;

namespace PencaUcuApi.Controllers;

[ApiController]
[Route("[controller]")]
/*
Endpoints:
GET
/get_future_matches
Params:     -
Devuelve:   200- Ok (Partidos, predicción o nulo) o 400- Bad request
Para ver las predicciones futuras, si no tiene predicción, que sea un nulo, y así se maneja

GET
/get_past_matches
Params:     -
Devuelve:   200- Ok (Partidos, predicción o nulo, resultado) o 400- Bad request
Partidos pasados, con su resultado y predicción

POST
/new_prediction
Params:     str match_id, int result_a, int result_b, str ci
Devuelve:   200- Ok o 400- Bad request

PUT
/change_prediction
Params:     str prediction_id, str match_id, int result_a, int result_b
Devuelve:   200- Ok o 400- Bad request
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

    [HttpGet]
    public IActionResult Get()
    {
        // TODO: Implement your logic here
        return Ok("Get method called");
    }

    [HttpGet("{id}")] // predictionId
    public async Task<IActionResult> GetItem(string id)
    {
        try
        {
            var query = await _dbContext
                .PredictionItemDTO.FromSqlRaw( // chequear
                    "SELECT M.LocalNationalTeam, P.LocalNationalTeamGoals, M.VisitorNationalTeam, P.VisitorNationalTeamGoals, M.Date, S.Name as StadiumName, S.State, S.City FROM Predictions as P INNER JOIN Matches as M ON P.MatchId = M.Id LEFT JOIN Stadiums as S ON M.StadiumId = S.Id WHERE P.Id = @id",
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
                predictionItemDTO.LocalNationalTeam
                    ?? throw new ArgumentNullException(nameof(predictionItemDTO.LocalNationalTeam)),
                predictionItemDTO.LocalNationalTeamFlagURL
                    ?? throw new ArgumentNullException(
                        nameof(predictionItemDTO.LocalNationalTeamFlagURL)
                    ),
                predictionItemDTO.LocalNationalTeamGoals
                    ?? throw new ArgumentNullException(
                        nameof(predictionItemDTO.LocalNationalTeamGoals)
                    ),
                predictionItemDTO.VisitorNationalTeam
                    ?? throw new ArgumentNullException(
                        nameof(predictionItemDTO.VisitorNationalTeam)
                    ),
                predictionItemDTO.VisitorNationalTeamFlagURL
                    ?? throw new ArgumentNullException(
                        nameof(predictionItemDTO.VisitorNationalTeamFlagURL)
                    ),
                predictionItemDTO.VisitorNationalTeamGoals
                    ?? throw new ArgumentNullException(
                        nameof(predictionItemDTO.VisitorNationalTeamGoals)
                    ),
                predictionItemDTO.Date,
                predictionItemDTO.StadiumName
                    ?? throw new ArgumentNullException(nameof(predictionItemDTO.StadiumName)),
                predictionItemDTO.State
                    ?? throw new ArgumentNullException(nameof(predictionItemDTO.State)),
                predictionItemDTO.City
                    ?? throw new ArgumentNullException(nameof(predictionItemDTO.City))
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
