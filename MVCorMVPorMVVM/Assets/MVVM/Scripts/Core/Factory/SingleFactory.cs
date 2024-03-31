using System;
using System.Collections.Generic;
using MVVM.Helper;

namespace MVVM.Factory
{
    /// <summary>
    /// 单例工厂
    /// 对象为单例，需要考虑线程安全
    /// </summary>
    public class SingleFactory : IFactory
    {
        /// <summary>
        /// 唯一字典,避免多个SingleFactory同时访问
        /// </summary>
        private static Dictionary<Type, object> cachedObjects = null;
        //只读 为了线程安全 避免线程锁对象改变
        private static readonly object _lock = new object();

        private Dictionary<Type, object> CachedObjects
        {
            get
            {
                //lock(this) 只对当前的对象有效果
                lock (_lock)
                {
                    if (cachedObjects == null)
                        cachedObjects = new Dictionary<Type, object>();
                    return cachedObjects;
                }
            }
        }
        public object AcquireObject(string className)
        {
            return AcquireObject(FactoryHelper.ResolveType(className));
        }

        public object AcquireObject(Type type)
        {
            if (CachedObjects.ContainsKey(type))
            {
                return CachedObjects[type];
            }

            lock (_lock)
            {
                CachedObjects.Add(type, Activator.CreateInstance(type,false));
                return CachedObjects[type];
            }
        }

        public object AcquireObject<IIstance>() where IIstance : class, new()
        {
            var type = typeof(IIstance);
            if (CachedObjects.ContainsKey(type))
            {
                return CachedObjects[type];
            }
            
            lock (_lock)
            {
                CachedObjects.Add(type, new IIstance());
                return CachedObjects[type];
            }
        }

        public void ReleaseObject(object obj)
        {
            throw new NotImplementedException();
        }
    }
}