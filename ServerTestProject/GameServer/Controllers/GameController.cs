using Microsoft.AspNetCore.Mvc;
using CommonLib;
using GameServer.Services;

namespace GameServer.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly PlayerService _playerService;

    public GameController(PlayerService playerService)
    {
        _playerService = playerService;
    }
    [HttpGet("{id}")]
    public Player Get([FromRoute] int id)
    {
        var player = new Player(){Id = id};
        _playerService.DoSomething();
        return player;
    }

    [HttpPost("[action]")]
    public Player Post(Player player)
    {
        return player;
    }
}