namespace CommonLib;

public class DbTest
{
    public class AddTestPlayerRequest
    {
        public Player player;
        public int num;
    }
    
    public class MongoDbRequest
    {
        public int num;
    }
    
    public class RedisRequest
    {
        public int num;
    }
    
    public class CommonTestResponse
    {
        public List<Player> players;
        public float timers;
    }
}