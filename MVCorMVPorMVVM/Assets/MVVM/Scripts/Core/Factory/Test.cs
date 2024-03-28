using System;
using UnityEngine;

namespace MVVM.Factory
{
    public class Test : IDisposable
    {
        public int index { get; set; }
        public Test()
        {
            
        }

        public void TestFactory()
        {
            Debug.Log("Temp Obj");
        }

        public void Dispose()
        {
            index = 0;
        }
    }
}