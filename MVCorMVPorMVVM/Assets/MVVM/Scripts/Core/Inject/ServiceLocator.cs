using System;
using System.Collections.Generic;
using MVVM.Factory;

namespace MVVM.Inject
{
    public class ServiceLocator
    {
        private static SingleFactory _singleFactory = new SingleFactory();
        private static TransientFactory _transientFactory = new TransientFactory();

        private static readonly Dictionary<Type, Func<object>> container = new Dictionary<Type, Func<object>>();

        /// <summary>
        /// 返回唯一的实例
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TInstance"></typeparam>
        public static void RegisterSingleton<TInterface, TInstance>() where TInstance : class, new()
        {
            container.Add(typeof(TInterface), Lazy<TInstance>(FactoryType.Singleton));
        }
        
        public static void RegisterSingleton<TInstance>() where TInstance : class, new()
        {
            container.Add(typeof(TInstance), Lazy<TInstance>(FactoryType.Singleton));
        }
        
        /// <summary>
        /// 返回不同的实例
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TInstance"></typeparam>
        public static void RegisterTransient<TInterface, TInstance>() where TInstance : class, new()
        {
            container.Add(typeof(TInterface), Lazy<TInstance>(FactoryType.Transient));
        }
        
        public static void RegisterTransient<TInstance>() where TInstance : class, new()
        {
            container.Add(typeof(TInstance), Lazy<TInstance>(FactoryType.Transient));
        }
        
        /// <summary>
        /// 清空
        /// </summary>
        public static void Clear()
        {
            container.Clear();
        }
        
        /// <summary>
        /// 获取容器中的实例
        /// </summary>
        /// <returns></returns>
        public static TInterface ResolveInstance<TInterface>() where TInterface : class
        {
            return ResolveInstance(typeof(TInterface)) as TInterface;
        }
        
        /// <summary>
        /// 获取容器中的实例
        /// </summary>
        /// <returns></returns>
        private static object ResolveInstance(Type type)
        {
            if (!container.ContainsKey(type))
                return null;
            return container[type]();
        }
        
        private static Func<object> Lazy<TInstance>(FactoryType factoryType) where TInstance : class, new()
        {
            return () =>
            {
                switch (factoryType)
                {
                    case FactoryType.Singleton:
                        return _singleFactory.AcquireObject<TInstance>();
                    default:
                        return _transientFactory.AcquireObject<TInstance>();
                }
            };
        }
    }
}