﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace YeluoFunc
{
    //为做基类而生的抽象类与开闭原则
    public class LuanQiBaZao
    {
        public static void Main(string[] args)
        {
            var han = new Han(new bike());
            //han.ride();

            // var bike = new bike();
            var t = System.Type.GetType("YeluoFunc.bike");
            object o = Activator.CreateInstance(t);
            MethodInfo ride = t.GetMethod("Run");
            ride.Invoke(o, null);
            // Iride ride = bike;
            // ride.Stop();
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
        void Iride.Stop()
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