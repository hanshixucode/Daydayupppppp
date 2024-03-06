using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace MVVM.Thread
{
    public class Dispatcher : MonoBehaviour
    {
        private int _lock;
        private bool _run;
        private Queue<Action> _wait;

        public void BeginInvoke(Action action)
        {
            while (true)
            {
                if (0 == Interlocked.Exchange(ref _lock, 1))
                {
                    _wait.Enqueue(action);
                    _run = true;
                    Interlocked.Exchange(ref _lock, 0);
                    break;
                }
            }
        }

        void Update()
        {
            if (_run)
            {
                Queue<Action> execute = null;
                //主线程不推荐使用lock关键字，防止block 线程，以至于deadlock
                if (0 == Interlocked.Exchange(ref _lock, 1))
                {
                    execute = new Queue<Action>(_wait.Count);

                    while (_wait.Count != 0)
                    {
                        Action action = _wait.Dequeue();
                        execute.Enqueue(action);
                    }

                    //finished
                    _run = false;
                    //release
                    Interlocked.Exchange(ref _lock, 0);
                }

                //not block
                if (execute != null)
                {
                    while (execute.Count != 0)
                    {
                        Action action = execute.Dequeue();
                        action();
                    }
                }
            }
        }
    }
}