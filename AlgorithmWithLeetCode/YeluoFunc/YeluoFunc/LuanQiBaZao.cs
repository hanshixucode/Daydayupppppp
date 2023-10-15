using System;
using System.Collections.Generic;

namespace YeluoFunc
{
    //为做基类而生的抽象类与开闭原则
    public class LuanQiBaZao
    {
        public static void Main(string[] args)
        {
            
        }
    }

    interface Icar
    {
        void Run();
        void Stop();
    }
    internal abstract class Car : Icar
    {
        public void Stop()
        {
            
        }
        public abstract void Run();
    }

    internal class bike : Car
    {
        public override void Run()
        {
            
        }
    }
}