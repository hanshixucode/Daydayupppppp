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
        // var playerinfo = new PlayerInfo() { id = player.id, health = player.health, level = player.level };
        // redis.HSet("health", playerinfo.id.ToString(), playerinfo);
        // redis.Expire("health", 3600);
        var playerinfo = new PlayerInfo() { id = player.id, health = player.health, level = player.level };
        redis.Set<PlayerInfo>($"{id}", playerinfo, 3600);

        Book book = new Book(){id = 1089,name = "asd123"};
        redis.HSet("book", book.id.ToString(), book);
        redis.Expire("book", 3600);
        
        // redis.JsonSet("freedis.test", System.Text.Json.JsonSerializer.Serialize(book));
        // var result = await redis.GetAsync<Player>($"{id}");
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
    
    [HttpPost("[action]")]
    public async Task<DbTest.CommonTestResponse> AddTestPlayer(DbTest.AddTestPlayerRequest request)
    {
        await Task.CompletedTask;
        var s = new System.Diagnostics.Stopwatch();
        s.Start();
        var response = new DbTest.CommonTestResponse();
        response.players = new List<Player>();
        for (int i = 0; i < request.num; i++)
        {
            var player = request.player.Clone();
            player.id = i;
            var playerDB = ImongoDb.GetCollection<PlayerInfo>("player");
            var filter = Builders<PlayerInfo>.Filter.Eq("id", player.id);
            var update = Builders<PlayerInfo>.Update.Set("level", player.level).Set("health", player.health);
            var options = new FindOneAndUpdateOptions<PlayerInfo>();
            options.IsUpsert = true;
            options.ReturnDocument = ReturnDocument.After;
            await playerDB.FindOneAndUpdateAsync(filter, update, options);
            response.players.Add(player);
        }
        s.Stop();
        response.timers = s.ElapsedMilliseconds;
        return response;
    }
    
    /// <summary>
    /// 直接从Mongo拿
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    public async Task<DbTest.CommonTestResponse> TestMongoDBGet(DbTest.MongoDbRequest request)
    {
        await Task.CompletedTask;
        var s = new System.Diagnostics.Stopwatch();
        s.Start();
        var response = new DbTest.CommonTestResponse();
        response.players = new List<Player>();
        var playerInfos = await FindAsync(request.num);
        foreach (var player in playerInfos)
        {
            response.players.Add(new Player(){id = player.id, health = player.health,level = player.level});
        }
        s.Stop();
        response.timers = s.ElapsedMilliseconds;
        return response;
    }

    private async Task<List<PlayerInfo>> FindAsync(int count)
    {
        var playerDB = ImongoDb.GetCollection<PlayerInfo>("player");
        var filter = Builders<PlayerInfo>.Filter.Eq("level", 5);
        var total = (int)await playerDB.CountDocumentsAsync(filter);
        var max = Math.Max(200, count);
        
        var result = await playerDB.FindAsync(filter, new FindOptions<PlayerInfo, int>()
        {
            Projection = Builders<PlayerInfo>.Projection.Expression(info => info.id),
            Limit = max,
        });

        var ids = await result.ToListAsync();
        return await GetNodeAsync(ids);
    }

    private async Task<List<PlayerInfo>> GetNodeAsync(List<int> list)
    {
        var playerDB = ImongoDb.GetCollection<PlayerInfo>("player");
        var filter = Builders<PlayerInfo>.Filter.In("id", list);
        var infos = await playerDB.FindAsync(filter);
        var result = new List<PlayerInfo>();

        await infos.ForEachAsync((info =>
        {
            result.Add(info);
        }));
        return result;
    }
    
    
    // [HttpPost("[action]")]
    // public async Task<DbTest.CommonTestResponse> TestRedisGet(DbTest.RedisRequest request)
    // {
    //     await Task.CompletedTask;
    //     var response = new Player.PlayerResponse();
    //     response.player = request.player;
    //     response.player.health += request.num;
    //     var player = response.player;
    //     var playerDB = ImongoDb.GetCollection<PlayerInfo>("player");
    //     var filter = Builders<PlayerInfo>.Filter.Eq("id", player.id);
    //     var update = Builders<PlayerInfo>.Update.Set("level", player.level).Set("health", player.health);
    //     var options = new FindOneAndUpdateOptions<PlayerInfo>();
    //     options.IsUpsert = true;
    //     options.ReturnDocument = ReturnDocument.After;
    //     await playerDB.FindOneAndUpdateAsync(filter, update, options);
    //     return response;
    // }
    
    public class Book
    {
        public int id;
        public string name;
    }
}