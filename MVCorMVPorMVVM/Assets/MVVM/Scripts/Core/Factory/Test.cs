using System;
using UnityEngine;

namespace MVVM.Factory
{
    public interface ITest
    {
        void TestFactory();
    }
    public class Test : ITest, IDisposable
    {
        public int index { get; set; }
        public Test(int ss)
        {
            
        }
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