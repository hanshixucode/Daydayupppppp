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
        var player = new Player(){id = id};
        _playerService.DoSomething();
        return player;
    }

    [HttpPost("[action]")]
    public async Task<Player.PlayerResponse> Post(Player.PlayerRequest request)
    {
        await Task.CompletedTask;
        var response = new Player.PlayerResponse();
        response.player = request.player;
        response.player.health += request.num;
        return response;
    }
}