using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Runtime.Loader;
using SDK;

namespace YeluoFunc
{
    // 依赖注入
    // [UnfinishedAttribute]
    public class LuanQiBaZao
    {
        public static void Main(string[] args)
        {
            var folder = Path.Combine(Environment.CurrentDirectory, "Animals");
            var files = Directory.GetFiles(folder);
            var animalTypes = new List<Type>();
            foreach (var file in files)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
                var types = assembly.GetTypes();
                foreach (var t in types)
                {
                    if (t.GetInterfaces().Contains(typeof(IAnimal)))
                    {
                        if(t.GetCustomAttributes(false).Any(a => a.GetType() == typeof(UnfinishedAttribute))) continue;
                        animalTypes.Add(t);
                    }
                }
            }

            while (true)
            {
                for (int i = 0; i < animalTypes.Count; i++)
                {
                    Console.WriteLine($"{i+1}.{animalTypes[i].Name}");
                }

                Console.WriteLine("chose");
                int index = int.Parse(Console.ReadLine());
                if (index > animalTypes.Count || index < 1)
                {
                    continue;
                }
                Console.WriteLine("choose times");
                int times = int.Parse(Console.ReadLine());
                var t = animalTypes[index - 1];
                var o = Activator.CreateInstance(t);
                var a = o as IAnimal;
                a.Voice(times);

            }

        }
    }
}