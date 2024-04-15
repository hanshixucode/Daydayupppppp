using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CommonLib.MongoDB;

public class MongoDBService : IMongoDb
{
    private IMongoDatabase mondoDb;
    
    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return mondoDb.GetCollection<T>(name);
    }
    
    // public MongoDBService(IServiceProvider provider, string type)
    // {
    //     
    // }
    public MongoDBService(IOptions<MongoDBSetting> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        mondoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
    }
}