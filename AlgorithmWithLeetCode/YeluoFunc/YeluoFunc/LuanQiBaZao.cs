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
        public Icar car;
        public Han(Icar car)
        {
            this.car = car;
        }
        public void ride()
        {
            car.Stop();
            car.Run();
        }
    }
    
    public interface Icar
    {
        void Run();
        void Stop();
    }
    internal abstract class Car : Icar
    {
        public void Stop()
        {
            Console.WriteLine("car");
        }
        public abstract void Run();
    }

    internal class bike : Car
    {
        public override void Run()
        {
            Console.WriteLine("bike");
        }
    }
}