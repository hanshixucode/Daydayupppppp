using System;
using System.Collections.Generic;

namespace MVVM.Repository
{
    /// <summary>
    /// 仓储模式的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, new()
    {
        void Insert(T instance);
        void Delete(T instance);
        void Update(T instance);
        IEnumerable<T> Select(Func<T, bool> func);
    }
}