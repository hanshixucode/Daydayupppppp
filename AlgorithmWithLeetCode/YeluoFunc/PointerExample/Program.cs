using System;
using static System.Console;

namespace PointerPlayground
{ 
    public unsafe class Program
    {
        public unsafe static void Main()
        {
            int x = 10;
            short y = -1;
            byte y2 = 4;
            double z = 1.5;
            int* pX = &x;
            short* pY = &y;
            double* pZ = &z;
            *pX = 20;
            
            //栈数组
            decimal* pDecimals = stackalloc decimal[10];
            double* pDoubles = stackalloc double[20];
            *pDoubles = 3.0;
            *(pDoubles + 1) = 4.0;
            pDoubles[0] = 3.0;
            pDoubles[1] = 4.0;
            Console.WriteLine($"{pDoubles[0]} + {pDoubles[50]}");
            var q = new Quick();
            q.QuickArray();
        }
    }

    internal class Quick
    {
        public unsafe void QuickArray()
        {
            string userInput = ReadLine();
            uint size = uint.Parse(userInput);

            long* pArray = stackalloc long[(int)size];
            for (int i = 0; i < size; i++)
            {
                pArray[i] = i * i;
                WriteLine($"{i} = {*(pArray + i)}");
            }

            ReadLine();
        }
    }
    
} 