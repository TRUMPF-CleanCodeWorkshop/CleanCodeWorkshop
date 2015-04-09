using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Contracts.Model;
using Core;
using DebugUI;
using Host.Properties;
using RobotEngineAdapter;

namespace Host
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var basePath = GetBasePath();
            var gameEnginePath =  Path.Combine(basePath , "engines");
            var robotEnginePath = Path.Combine(basePath, "robots");

            var configuration = new GameConfiguration() {MapSize = Settings.Default.MapSize, PowerupPropability = Settings.Default.PowerUpPropability};
            var gameEngine = GameEngineAdapter.EngineLoader.Load(gameEnginePath);
            var robotEngines = RobotLoader.Load(robotEnginePath);
            var gameState = Framework.CreateInitializeGameState(configuration, gameEngine, robotEngines);
            
            GameUI.ShowGameState(gameState);
            Console.ReadLine();
        }

        private static string GetBasePath()
        {
            var file = new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            // ReSharper disable once PossibleNullReferenceException
            return file.Directory.FullName;
        }
    }
}
