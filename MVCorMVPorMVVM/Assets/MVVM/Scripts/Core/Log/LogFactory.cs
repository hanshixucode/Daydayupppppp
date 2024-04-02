using System.Collections.Generic;

namespace MVVM.Log
{
    public class LogFactory
    {
        public static LogFactory Instacne = new LogFactory();

        private readonly Dictionary<string, LogStrategy> _strategies = new Dictionary<string, LogStrategy>()
        {
            { typeof(ConsoleLogStrategy).Name, new ConsoleLogStrategy() }
        };

        public LogStrategy Resolve<T>() where T : LogStrategy
        {
            return _strategies[typeof(T).Name];
        }
        
    }
}