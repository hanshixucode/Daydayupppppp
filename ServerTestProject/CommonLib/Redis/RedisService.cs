using FreeRedis;
using Microsoft.Extensions.Options;

namespace CommonLib.Redis;

public class RedisService
{
    private RedisClient redisDB;
    
    public RedisClient GetRedisClient => redisDB;
    
    public RedisService(IOptions<RedisSetting> settings)
    {
        redisDB = new RedisClient($"{settings.Value.Url},password={settings.Value.Pass},defaultDatabase={settings.Value.DefaultDatabase}");
        redisDB.Notice += (sender, args) => Console.WriteLine(args.Log);
    }
}