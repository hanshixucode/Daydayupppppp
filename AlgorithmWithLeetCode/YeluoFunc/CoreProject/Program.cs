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
            sc.AddScoped<ICar, Gti>();
            var sp = sc.BuildServiceProvider();

            ICar car = sp.GetService<ICar>();
            car.Run();
        }
    }

    interface ICar
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
}