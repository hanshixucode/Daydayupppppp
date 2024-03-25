using System;
using MVVM.Helper;

namespace MVVM.Factory
{
    /// <summary>
    /// 临时对象工厂
    /// 所获取的对象都是随用随销毁
    /// 目前都是托管资源工厂...
    /// </summary>
    public class TransientFactory : IFactory
    {
        public object AcquireObject(string className)
        {
            return AcquireObject(FactoryHelper.ResolveType(className));
        }

        public object AcquireObject(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public object AcquireObject<IIstance>() where IIstance : class, new()
        {
            return new IIstance();
        }

        public void ReleaseObject(object obj)
        {
            //托管资源.NET回收
        }
    }
}