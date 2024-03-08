using System.Reflection.Metadata.Ecma335;

namespace ThreadProject;

public class ThreadStateMachine
{
    
    public interface IStateObject
    {
        void EnterState();
        void ExitState();
        void UpdateState();
    }
    
    public class StateMachine
    {
        public int runInterval = 500;
        private string currentState;
        private Dictionary<string, IStateObject> stateObjectsDic = new Dictionary<string, IStateObject>();
        private Thread _thread;
        private bool isRun = false;

        public StateMachine(int runInterval = 500)
        {
            this.runInterval = runInterval;
        }

        public void Register(string name, IStateObject stateObject)
        {
            stateObjectsDic.Add(name, stateObject);
        }

        public void SetState(string name)
        {
            if (currentState != name)
            {
                if (currentState != null && stateObjectsDic.TryGetValue(currentState, out var oldObj))
                {
                    oldObj.ExitState();
                }

                currentState = name;
                if (currentState != null && stateObjectsDic.TryGetValue(currentState, out var newObj))
                {
                    newObj.EnterState();
                }
            }
        }

        public void Start()
        {
            if (!isRun)
            {
                isRun = true;
                _thread = new Thread(new ThreadStart(Run));
                _thread.IsBackground = true;
                _thread.Start();
                Console.WriteLine("状态机启动");
            }
        }

        public void Close()
        {
            if (isRun)
            {
                if (currentState != null && stateObjectsDic.TryGetValue(currentState, out var oldObj))
                {
                    oldObj.ExitState();
                }

                isRun = false;

                try
                {
                    _thread.Interrupt();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                Thread.Sleep(50);
                _thread = null;
                Console.WriteLine("状态机停止");
            }
            
        }

        private void Run()
        {
            try
            {
                while (isRun)
                {
                    Update();
                    SpinWait.SpinUntil((() => !isRun), runInterval);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void Update()
        {
            if (currentState != null && stateObjectsDic.TryGetValue(currentState, out var oldObj))
            {
                oldObj.UpdateState();
            }
        }
    }
}