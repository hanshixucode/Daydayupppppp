using UnityEngine;

namespace MVVM.Log
{
    public class ConsoleWriter : IContenWriter
    {
        public void Write(string message)
        {
            Debug.Log("log: " + message);
        }
    }
}