using System;
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

            //可空类型NULL
            string tempNull = null;
            var test = tempNull ?? "nope";

            A a = new A();
            B b = new B();
            C c = new C();
        }

        //引用类型传递
        public static void ChangeAge(ref PeopleParme peopleParme)
        {
            peopleParme.Age = 1;
        }

    }

    //拓展方法
    public static class StringExtension
    {
        public static int GetWordCount(this string s) => s.Length;
        public static int GetId(this Han han) => han.id; 
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

        ~Han()
        {
            Console.WriteLine("end");
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

    public class A
    {
        public A()
        {
            Console.WriteLine("A");
        }
    }

    public class B : A
    {
        //派生构造函数
        public B() : base()
        {
            Console.WriteLine("B");
        }
    }

    public class C : B
    {
        public C()
        {
            Console.WriteLine("C");
        }
    }
    
}