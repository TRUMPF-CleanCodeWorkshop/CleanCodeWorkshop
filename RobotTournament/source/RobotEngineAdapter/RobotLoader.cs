using Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RobotEngineAdapter
{
    public class RobotLoader
    {
        public static IEnumerable<IRobotEngine> Load(string path)
        {
            var files = Directory.GetFiles(path, "*.dll");

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;

            var assemblies = files.Select(Assembly.ReflectionOnlyLoadFrom);
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var typesWithInterface = types.Where(t => t.GetInterfaces().Any(i => i.FullName == typeof(IRobotEngine).FullName)).ToList();


            if (typesWithInterface.Count == 0)
            {
                throw new Exception("IRobotEngine in keiner DLL gefunden");
            }

            return typesWithInterface.Select(f => (IRobotEngine)Assembly.LoadFrom(f.Assembly.CodeBase).CreateInstance(f.FullName)).ToList();
        }

        private static Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.ReflectionOnlyLoad(args.Name);
        }
    }
}
