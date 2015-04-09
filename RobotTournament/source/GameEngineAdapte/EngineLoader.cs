using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace GameEngineAdapter
{
    public class EngineLoader
    {
        public static IGameEngine Load(string path)
        {
            var files = Directory.GetFiles(path, "*.dll");

            var assemblies = files.Select(Assembly.ReflectionOnlyLoadFrom);
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var typesWithInterface = types.Where(t => t.GetInterfaces().Any(i => i.FullName == typeof (IGameEngine).FullName)).ToList();

            if (typesWithInterface.Count == 0)
            {
                throw new Exception("IGameInterface in keiner DLL gefunden");
            }

            var typeToLoad = typesWithInterface.First();

            return (IGameEngine)Assembly.LoadFrom(typeToLoad.Assembly.CodeBase).CreateInstance(typeToLoad.FullName);
        }

    }
}
