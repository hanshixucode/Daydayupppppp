﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

            Box<Apple> box = new Box<Apple>(){cargo = new Apple(){Color = "red"}};
            Box<Book> box2 = new Box<Book>(){cargo = new Book(){Name = "laji"}};
            Console.WriteLine(box.cargo.Color);
            Console.WriteLine(box2.cargo.Name);

            var han = new Han("hanshixu", 123);
            han.OnGet += delegate(string name, int id)
            {
                Console.WriteLine($"good {name}");
            };
            han.GetNameEvent();

            var xx = new PeopleParme(26);
            xx.HappyBirthDay(out int tempAge);
            ChangeAge(ref xx);
            xx.GetAge();
            Console.WriteLine(tempAge);
        }

        //引用类型传递
        public static void ChangeAge(ref PeopleParme peopleParme)
        {
            peopleParme.Age = 1;
        }

    }

    //结构体
    public interface IPeopleParme
    {
        void GetAge();
    }
    public struct PeopleParme : IPeopleParme
    {
        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        public PeopleParme(int age)
        {
            this.age = age;
        }

        public void HappyBirthDay(out int result)
        {
            Age++;
            result = Age;
        }
        public void GetAge()
        {
            Console.WriteLine($"age = {age}");
        }
    }

    public interface People<T1, T2>
    {
        T1 Name();
        T2 Id();
    }

    public class Han : People<string, int>
    {
        public const string defaultName = "Hanshixu";
        public delegate void GetName(String name, int id);
        public event GetName OnGet;
        public void GetNameEvent()
        {
            OnGet?.Invoke(name, id);
            Console.WriteLine(Level);
        }

        public string name;
        public int id;

        private string level = "10";
        public string Level
        {
            get;
        }
        public Han(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
        
        public string Name()
        {
            return name;
        }

        public int Id()
        {
            return id;
        }
    }


    public class Box<T>
    {
        public T cargo { get; set; }
    }

    public class Apple
    {
        public string Color { get; set; }
    }
    public class Book
    {
        public string Name { get; set; }
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