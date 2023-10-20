using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace YeluoFunc
{
    //依赖注入
    public class LuanQiBaZao
    {
        public static void Main(string[] args)
        {
            var sc = new ServiceCollection();
            sc.AddScoped<ICar, Shadow>();
            sc.AddScoped<ICar, Gti>();
            sc.AddScoped<Driver>();
            var sp = sc.BuildServiceProvider();
            
            ICar car = sp.GetService<ICar>();
            Driver driver = sp.GetService<Driver>();
            car.Run();
            driver.Drive();
        }
    }

    public class Driver
    {
        private ICar car;
        public Driver(ICar car)
        {
            this.car = car;
        }

        public void Drive()
        {
            car.Run();
        }
    }
    
    public interface ICar
    {
        void Run();
    }

    public class Gti : ICar
    {
        public void Run()
        {
            Console.WriteLine("weng weng weng");
        }
    }
    
    public class Shadow : ICar
    {
        public void Run()
        {
            Console.WriteLine("ang ang ang");
        }
    }
}