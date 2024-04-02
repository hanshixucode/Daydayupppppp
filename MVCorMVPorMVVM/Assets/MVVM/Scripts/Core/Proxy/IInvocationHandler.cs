using System.Reflection;

namespace MVVM.Proxy
{
    public interface IInvocationHandler
    {
        void PreProcess();
        object Invoke(object proxy, MethodInfo methodInfo, object[] args);
        void PostProcess();
    }
}