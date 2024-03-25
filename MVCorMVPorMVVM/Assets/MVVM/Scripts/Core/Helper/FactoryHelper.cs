using System;
using System.Linq;
using System.Reflection;

namespace MVVM.Helper
{
    public class FactoryHelper
    {
        public static Type ResolveType(string className)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == className);
            if (type == null)
            {
                throw new Exception(string.Format("cant find class {0}", className));
            }

            return type;
        }
    }
}