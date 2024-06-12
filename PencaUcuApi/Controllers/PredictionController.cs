using Microsoft.AspNetCore.Mvc;

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
