using System.Reflection;
using MVVM.Log;

namespace MVVM.Proxy
{
    public class LogInvocationHandler : IInvocationHandler
    {
        public void PreProcess()
        {
            LogFactory.Instacne.Resolve<ConsoleLogStrategy>().Log("pre");
        }

        public object Invoke(object proxy, MethodInfo methodInfo, object[] args)
        {
            PreProcess();
            var result = methodInfo.Invoke(proxy, args);
            PostProcess();
            return result;
        }

        public void PostProcess()
        {
            LogFactory.Instacne.Resolve<ConsoleLogStrategy>().Log("post");
        }
    }
}