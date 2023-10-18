using System;
using System.Collections.Generic;

namespace YeluoFunc
{
    //为做基类而生的抽象类与开闭原则
    public class LuanQiBaZao
    {
        public static void Main(string[] args)
        {
            var han = new Han(new bike());
            han.ride();
        }
    }

    public interface Driver
    {
        void ride();
    }

    public class Han : Driver
    {
        public Imusic car;
        public Han(Imusic car)
        {
            this.car = car;
        }
        public void ride()
        {
            car.Play();
        }
    }

    public interface Iride
    {
        void Run();
        void Stop();
    }
    
    public interface Icar : Iride, Imusic
    {
        
    }

    public interface Imusic
    {
        void Play();
    }
    internal abstract class Car : Icar
    {
        public void Stop()
        {
            Console.WriteLine("car");
        }
        public abstract void Run();
        public abstract void Play();
    }

    internal class bike : Car
    {
        public override void Run()
        {
            Console.WriteLine("bike");
        }
        
        public override void Play()
        {
            Console.WriteLine("music bike");
        }
    }
}