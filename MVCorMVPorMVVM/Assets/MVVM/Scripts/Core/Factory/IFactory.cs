using System;

namespace MVVM.Factory
{
    public enum FactoryType
    {
        Singleton,
        Transient,
        Pool
    }
    public interface IFactory
    {
        object AcquireObject(string className);
        object AcquireObject(Type type);
        object AcquireObject<IIstance>() where IIstance : class, new();
        void ReleaseObject(object obj);
    }
}