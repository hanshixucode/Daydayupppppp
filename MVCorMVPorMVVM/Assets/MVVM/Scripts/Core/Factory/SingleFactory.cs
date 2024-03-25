using System;

namespace MVVM.Factory
{
    /// <summary>
    /// 单例工厂
    /// 对象为单例，需要考虑线程安全
    /// </summary>
    public class SingleFactory : IFactory
    {
        public object AcquireObject(string className)
        {
            throw new NotImplementedException();
        }

        public object AcquireObject(Type type)
        {
            throw new NotImplementedException();
        }

        public object AcquireObject<IIstance>() where IIstance : class, new()
        {
            throw new NotImplementedException();
        }

        public void ReleaseObject(object obj)
        {
            throw new NotImplementedException();
        }
    }
}