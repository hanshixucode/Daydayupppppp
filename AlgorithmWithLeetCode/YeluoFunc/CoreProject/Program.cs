﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YeluoFunc
{
    public class StringTest
    {
        //自定义字符串格式
        public class Person : IFormattable
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public override string ToString()
            {
                return FirstName + "" + LastName;
            }
            public virtual string ToString(string format)
            {
                return ToString(format, null);
            }
            public string ToString(string? format, IFormatProvider? formatProvider)
            {
                switch (format)
                {
                    case null:
                    case "A":
                        return ToString();
                    case "F":
                        return FirstName;
                    case "L":
                        return LastName;
                    default:
                        throw new FormatException($"invalid format string{format}");
                }
            }
        }
        public static void Main(string[] args)
        {
            var ss = new StringBuilder("123123123123123", 200);
            ss.AppendFormat("qasd");
            Console.WriteLine(ss);

            var p1 = new Person() { FirstName = "han",LastName = "xu"};
            Console.WriteLine(p1.ToString("A"));

            //正则表达式
            string input = @"onqweasdasfdasddasdqwonsdadasd" +
                           "asd asd asd asd  on asd wa dsa d.";
            string pattern = "on";
            MatchCollection matches =
                Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            foreach (Match nextMatch in matches)
            {
                Console.WriteLine(nextMatch.Index);
            }
        }
    }
    //操作符与类型转换
    public unsafe class Operator
    {
        public static void Main2(string[] args)
        {
            Object o = 5;
            string s = o as string;
            Console.WriteLine(s);
            
            int k = 0;
            int* i = &k;
            int? j = null;
            j += ++*i; 
            // k j？
            Console.WriteLine($"{k.ToString()} {j ?? 1}");

            // long longnumber = 123212312;
            // object longstring = (object)longnumber;
            // int intnumber = (int)longstring;
            // 强制转换的bug

        }
    }
    //依赖注入
    public class LuanQiBaZao
    {
        public static void Main1(string[] args)
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
            
            using (var ResourceHolder = new ResourceHolder())
            {
                //异常抛出不会影响Dispose
                Console.WriteLine("doing");
                //throw new Exception("异常抛出");
            }
            // {
            //     不会触发Dispose
            //     var instance = new A();
            //     Console.WriteLine("doing");
            //     throw new Exception("异常抛出");
            //     instance.Dispose();
            // }
            
        }

        //引用类型传递
        public static void ChangeAge(ref PeopleParme peopleParme)
        {
            peopleParme.Age = 1;
        }

    }

    //指针
    unsafe class UnsafeClass
    {
        private int* pWidth, pHeight;
        private double* pResult;
        private byte*[] pFlags;
    }

    public unsafe class Pointer
    {
        struct MyStruct
        {
            public long X;
            public float F;
        }

        protected internal class MyClass
        {
            public long X;
            public float F;
        }

        private MyStruct* pStruct;
        
        public void Test()
        {
            int x = 10;
            int* pX, pY;
            pX = &x; //寻址
            pY = pX; 
            *pY = 20;//间接寻址
            ulong y = (ulong)pX;
            int* pD = (int*)y;
            Console.WriteLine($"{x}");
            Console.WriteLine($"{(ulong)pX}");
            // 20;

            //结构体指针
            var myStruct = new MyStruct();
            pStruct = &myStruct;
            (*pStruct).X = 4;
            pStruct->X = 4;
            var pL = &(myStruct.X);
            
            //类成员指针
            var myObj = new MyClass();
            //long* pL = &(myObj.X);
            fixed (long* pObj = &(myObj.X))
            {
                //do something...
            }

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

    public class A : IDisposable
    {

        public void Dispose()
        {
            Console.WriteLine("dispose");
        }
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

    /// <summary>
    /// IDisposable 以及析构
    /// </summary>
    public class ResourceHolder : IDisposable
    {
        private bool _isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!_isDisposed)
            {
            {
                if (disposing)
                {
                    //dispose 托管资源
                }
            }}
            //释放非托管资源
            _isDisposed = true;
        }

        ~ResourceHolder()
        {
            Dispose(false);
        }

        public void SomeMethod()
        {
            //something do...
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ResourceHolder");
            }
            
            //method implementation...
        }
    }
}