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

            var assemblies = files.Select(Assembly.ReflectionOnlyLoadFrom);
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var typesWithInterface = types.Where(t => t.GetInterfaces().Any(i => i.FullName == typeof(IRobotEngine).FullName)).ToList();


            if (typesWithInterface.Count == 0)
            {
                throw new Exception("IRobotEngine in keiner DLL gefunden");
            }

            return typesWithInterface.Select(f => { var robot = (IRobotEngine)Assembly.LoadFrom(f.Assembly.CodeBase).CreateInstance(f.FullName); return robot; });

            /*
             List<IRobotEngine> robotInstances = new List<IRobotEngine>();

            foreach (var singleTypeWithInterface in typesWithInterface)
            {
                var singleIRobotInstance = (IRobotEngine)Assembly.LoadFrom(singleTypeWithInterface.Assembly.CodeBase).CreateInstance(singleTypeWithInterface.FullName);
                robotInstances.Add(singleIRobotInstance);
            }

            return robotInstances; */
        }
    }
}
