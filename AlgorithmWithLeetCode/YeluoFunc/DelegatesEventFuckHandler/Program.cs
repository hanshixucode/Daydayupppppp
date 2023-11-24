using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Linq;

namespace FuckC
{
    internal class MainClass
    {
        public static void Main()
        {
            List<int> array = new List<int>()
            {
                1, 2, 8, 2, 5
            };
            BubbleSorter.Sort(array, delegate(int a, int b)
            {
                return a > b;
            });
            foreach (var a in array)
            {
                Console.WriteLine(a);
            }
            
            //闭包 委托内部调用外部变量
            int ss = 5;
            Func<int, int> f = i => i + ss;
            ss = 7;
            Console.WriteLine(f(3));
            
            //事件
            var han = new CarDealer();
            var zhang = new ConSumer("xiaozhang");
            han.NewCarInfo += zhang.NewCarIsHere;
            han.NewCar("Gti");
        }
    }

    public class CarInfoEventArgs : EventArgs
    {
        private string car;

        public string Car
        {
            get
            {
                return car;
            }
            private set
            {
                car = value;
            }
        }

        public CarInfoEventArgs(string car)
        {
            Car = car;
        }
    }

    public class ConSumer
    {
        private string _name;

        public ConSumer(string name)
        {
            _name = name;
        }

        public void NewCarIsHere(object sender, CarInfoEventArgs e)
        {
            Console.WriteLine($"{_name} is here {e.Car}");
        }
        
    }
    public class CarDealer
    {
        private EventHandler<CarInfoEventArgs> newCarInfo;
        public event EventHandler<CarInfoEventArgs> NewCarInfo
        {
            add
            {
                newCarInfo += value;
            }
            remove
            {
                newCarInfo -= value;
            }
        }

        public void NewCar(string car)
        {
            newCarInfo?.Invoke(this, new CarInfoEventArgs(car));
        }
    }
    internal class BubbleSorter
    {
        delegate void IntInvoker(int x);

        public static void Sort<T>(IList<T> sortArray, Func<T, T, bool> comparison)
        {
            bool isSwapped = true;
            do
            {
                isSwapped = false;
                for (int i = 0; i < sortArray.Count - 1; i++)
                {
                    if (comparison(sortArray[i], sortArray[i + 1]))
                    {
                        var temp = sortArray[i];
                        sortArray[i] = sortArray[i + 1];
                        sortArray[i + 1] = temp;
                        isSwapped = true;
                    }
                }
                
            } while (isSwapped);
        }
    }
}
