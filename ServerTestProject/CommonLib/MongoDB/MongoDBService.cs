using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CommonLib.MongoDB;

public class MongoDBService : IMongoDb
{
    private IMongoDatabase mongoDb;
    
    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return mongoDb.GetCollection<T>(name);
    }
    
    // public MongoDBService(IServiceProvider provider, string type)
    // {
    //     
    // }
    public MongoDBService(IOptions<MongoDBSetting> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
    }
}