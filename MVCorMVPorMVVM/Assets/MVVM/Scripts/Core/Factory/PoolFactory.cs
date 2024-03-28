using System;
using System.Collections.Generic;
using MVVM.Helper;

namespace MVVM.Factory
{
    /// <summary>
    /// 对象池工厂
    /// 生产对象用完后会回收
    /// </summary>
    public class PoolFactory : IFactory
    {
        /// <summary>
        /// 对象
        /// </summary>
        private class PoolData
        {
            public bool InUse { get; set; }
            public object Obj { get; set; }
        }

        private readonly List<PoolData> poolList;
        /// <summary>
        /// 最大容量
        /// </summary>
        private readonly int maxNum;
        //限制
        private readonly bool isLimit;

        public PoolFactory(int _maxNum, bool _isLimit)
        {
            maxNum = _maxNum;
            isLimit = _isLimit;
            poolList = new List<PoolData>();
        }

        private PoolData GetData(object obj)
        {
            lock (poolList)
            {
                for (int i = 0; i < poolList.Count; i++)
                {
                    var p = poolList[i];
                    if (p.Obj == obj)
                        return p;
                }
            }

            return null;
        }

        private object GetPoolObject(Type type)
        {
            lock (poolList)
            {
                if (poolList.Count > 0)
                {
                    //类型一致
                    if (poolList[0].Obj.GetType() != type)
                    {
                        throw new Exception(
                            string.Format("this pool type is {0}", poolList[0].Obj.GetType().Name));
                    }
                }

                for (int i = 0; i < poolList.Count; i++)
                {
                    var p = poolList[i];
                    if (!p.InUse)
                    {
                        p.InUse = true;
                        return p.Obj;
                    }
                }

                if (poolList.Count >= maxNum && isLimit)
                {
                    throw new Exception("pool num limit");
                }
                
                object obj = Activator.CreateInstance(type, false);
                var poolData = new PoolData()
                {
                    InUse = true,
                    Obj = obj
                };
                poolList.Add(poolData);
                return obj;
            }
        }

        private void RecycleObj(object obj)
        {
            var p = GetData(obj);
            if (p != null)
                p.InUse = false;
        }
        
        
        public object AcquireObject(string className)
        {
            return AcquireObject(FactoryHelper.ResolveType(className));
        }

        public object AcquireObject(Type type)
        {
            return GetPoolObject(type);
        }

        public object AcquireObject<IIstance>() where IIstance : class, new()
        {
            return AcquireObject(typeof(IIstance));
        }

        public void ReleaseObject(object obj)
        {
            if (poolList.Count > maxNum)
            {
                if(obj is IDisposable)
                    ((IDisposable)obj).Dispose();
                var p = GetData(obj);
                lock (poolList)
                {
                    poolList.Remove(p);
                }
                return;
            }
            RecycleObj(obj);
        }
    }
}