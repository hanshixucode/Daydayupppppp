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
            
        }
    }

    internal class BubbleSorter
    {
        delegate void IntInvoker(int x);

        public void Sort<T>(IList<T> sortArray, Func<T, T, bool> comparison)
        {
            bool isSwapped = true;
            do
            {
                isSwapped = false;
                for (int i = 0; i < sortArray.Count; i++)
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
