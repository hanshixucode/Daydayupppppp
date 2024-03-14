using System;
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
            var temp = new List<int>() { 1, 3, 2, 4, 5, 7, 6 };
            QuickSort(temp, 0, temp.Count -1);
        }
        public static List<int> BubbleSort(List<int> list)
        {
            int num = list.Count - 1;
            int temp = 0;
            for (int i = num; i > 0; i--)
            {
                for (int j = 0; j < num; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
            return list;
        }

        public static void QuickSort(List<int> list, int low, int high)
        {
            if (low > high)
            {
                return;
            }

            int i = low;
            int j = high;
            int temp = list[i];
            while (i < j)
            {
                while (i<j && temp < list[j])
                {
                    j--;
                }

                list[i] = list[j];

                while (i<j && temp >= list[j])
                {
                    i++;
                }

                list[j] = list[i];
            }

            list[i] = temp;
            QuickSort(list, low, i-1);
            QuickSort(list, j+1 ,high);
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