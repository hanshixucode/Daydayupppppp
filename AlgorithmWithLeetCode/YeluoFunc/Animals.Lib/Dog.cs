using SDK;

namespace Animals.Lib;

public class Dog : IAnimal
{
    public void Voice(int times)
    {
        for (int i = 0; i < times; i++)
        {
            Console.WriteLine("wang wang wang");
        }
    }
}