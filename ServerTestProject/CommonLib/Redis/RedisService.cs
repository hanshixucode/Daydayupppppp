using FreeRedis;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CommonLib.Redis;

public class RedisService
{
    private RedisClient redisDB;
    
    public RedisClient GetRedisClient => redisDB;
    
    public RedisService(IOptions<RedisSetting> settings)
    {
        redisDB = new RedisClient($"{settings.Value.Url},password={settings.Value.Pass},defaultDatabase={settings.Value.DefaultDatabase}");
        redisDB.Serialize = o => JsonConvert.SerializeObject(o);
        redisDB.Deserialize = (s, type) => JsonConvert.DeserializeObject(s, type);
        redisDB.Notice += (sender, args) => Console.WriteLine(args.Log);
    }
}