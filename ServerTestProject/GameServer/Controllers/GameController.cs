using Microsoft.AspNetCore.Mvc;
using CommonLib;
using CommonLib.Models;
using CommonLib.MongoDB;
using CommonLib.Redis;
using FreeRedis;
using GameServer.Services;
using MongoDB.Driver;

namespace GameServer.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly PlayerService _playerService;
    private IMongoDb ImongoDb;
    private RedisClient redis;
    public GameController(PlayerService playerService, MongoDBService mongoDbService, RedisService redisService)
    {
        _playerService = playerService;
        ImongoDb = mongoDbService;
        redis = redisService.GetRedisClient;
    }
    [HttpGet("{id}")]
    public async Task<Player> Get([FromRoute] int id)
    {
        Player player = new Player(){id = id};
        _playerService.DoSomething();
        var playerDB = ImongoDb.GetCollection<PlayerInfo>("player");
        var filter = Builders<PlayerInfo>.Filter.Eq("id", player.id);
        var update = Builders<PlayerInfo>.Update.Set("level", player.level).Set("health", player.health);
        var options = new FindOneAndUpdateOptions<PlayerInfo>();
        options.IsUpsert = true;
        options.ReturnDocument = ReturnDocument.After;
        await playerDB.FindOneAndUpdateAsync(filter, update, options);
        
        //测试redis
        await redis.SetAsync<Player>($"{id}", player, 3600);
        var result = await redis.GetAsync<Player>($"{id}");
        return player;
    }

    [HttpPost("[action]")]
    public async Task<Player.PlayerResponse> Post(Player.PlayerRequest request)
    {
        await Task.CompletedTask;
        var response = new Player.PlayerResponse();
        response.player = request.player;
        response.player.health += request.num;
        var player = response.player;
        var playerDB = ImongoDb.GetCollection<PlayerInfo>("player");
        var filter = Builders<PlayerInfo>.Filter.Eq("id", player.id);
        var update = Builders<PlayerInfo>.Update.Set("level", player.level).Set("health", player.health);
        var options = new FindOneAndUpdateOptions<PlayerInfo>();
        options.IsUpsert = true;
        options.ReturnDocument = ReturnDocument.After;
        await playerDB.FindOneAndUpdateAsync(filter, update, options);
        return response;
    }
}