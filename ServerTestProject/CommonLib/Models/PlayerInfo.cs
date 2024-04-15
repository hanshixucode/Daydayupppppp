using MongoDB.Bson.Serialization.Attributes;

namespace CommonLib.Models;

public class PlayerInfo
{
    [BsonId] 
    public int id { get; set; }
    public int level { get; set; }
    public float health { get; set; }
}