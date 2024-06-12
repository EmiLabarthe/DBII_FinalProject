using Microsoft.AspNetCore.Mvc;

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
public class FixtureController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    private readonly ILogger<UserController> _logger;

    public FixtureController(ILogger<UserController> logger, MyDbContext dbContext)
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
