namespace CommonLib;

public partial class Player
{
    public int id;
    public int level;
    public float health;

    public Player Clone()
    {
        return new Player() { id = id, level = level, health = health };
    }
}