using Microsoft.AspNetCore.Mvc;

namespace PencaUcuApi.Controllers;
[ApiController]
[Route("[controller]")]

/*
Endpoints:
POST
/new_match	
Params:     str id_a_team, str id_b_team, str id_phase	
Devuelve:   200- Ok o 400- Bad request / 401- Unauthorized	
Ingresa nuevo partido

POST
/match_result
Params:     str id_a_team, str id_b_team, boolean groups
Devuelve:   200- Ok o 400- Bad request / 401- Unauthorized
Ingresa resultado partido. Si es de grupos, cambia las estadísticas de las selecciones

POST
/end_tournament	
Params:     -	
Devuelve:   200- Ok o 400- Bad request / 401- Unauthorized	
Termina penca, debería actualizar puntos por acierto de campeon / subcampeon
*/
public class AdminController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    private readonly ILogger<UserController> _logger;

    public AdminController(ILogger<UserController> logger, MyDbContext dbContext)
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
