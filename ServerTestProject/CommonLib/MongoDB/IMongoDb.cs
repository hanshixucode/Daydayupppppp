using MongoDB.Driver;

namespace CommonLib.MongoDB;

public interface IMongoDb
{
    IMongoCollection<T> GetCollection<T>(string name);
}