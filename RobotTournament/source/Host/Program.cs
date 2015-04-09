using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var basePath = GetBasePath();
            var gameEnginePath =  Path.Combine(basePath , "engines");
            var robotEnginePath = Path.Combine(basePath, "robots");

            var gameEngine = GameEngineAdapter.EngineLoader.Load(gameEnginePath);
            

        }

        private static string GetBasePath()
        {
            var file = new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            // ReSharper disable once PossibleNullReferenceException
            return file.Directory.FullName;
        }
    }
}
