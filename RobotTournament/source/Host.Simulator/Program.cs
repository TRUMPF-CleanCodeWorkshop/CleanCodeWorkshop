using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Contracts.Model;
using GameEngineAdapter;
using RobotEngineAdapter;
using Simulation;

namespace Host.Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var basePath = GetBasePath();
            var gameEnginePath = Path.Combine(basePath, "engines");
            var robotEnginePath = Path.Combine(basePath, "robots");

            var configuration = new GameConfiguration() { MapSize = Settings.Default.MapSize, PowerupPropability = Settings.Default.PowerUpPropability, RobotStartLevel = Settings.Default.RobotStartLevel };
            var gameEngine = EngineLoader.Load(gameEnginePath);
            var robotEngines = RobotLoader.Load(robotEnginePath).ToList();

            var simulator = new BattleSimulator();

            var result = simulator.Simulate(configuration, gameEngine, robotEngines);

        }

        private static string GetBasePath()
        {
            var file = new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            // ReSharper disable once PossibleNullReferenceException
            return file.Directory.FullName;
        }
    }
}
