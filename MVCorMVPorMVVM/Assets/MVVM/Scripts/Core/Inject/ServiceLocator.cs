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
        
        
    }
}