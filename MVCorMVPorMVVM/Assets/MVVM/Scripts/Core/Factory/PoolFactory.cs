﻿using System;

namespace MVVM.Factory
{
    /// <summary>
    /// 对象池工厂
    /// 生产对象用完后会回收
    /// </summary>
    public class PoolFactory : IFactory
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