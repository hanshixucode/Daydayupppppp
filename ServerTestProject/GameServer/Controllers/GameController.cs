using Microsoft.AspNetCore.Mvc;
using CommonLib;
namespace GameServer.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    [HttpGet("{id}")]
    public Player Get([FromRoute] int id)
    {
        var player = new Player(){Id = id};
        return player;
    }

    [HttpPost("[action]")]
    public Player Post(Player player)
    {
        return player;
    }
}