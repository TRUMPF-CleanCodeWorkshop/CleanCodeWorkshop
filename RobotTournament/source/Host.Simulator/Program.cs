using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Contracts.Model;
using GameEngineAdapter;
using Host.Simulator.Properties;
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


            var result = BattleSimulator.Simulate(configuration, gameEngine, robotEngines, 30);

            foreach (var team in result.TeamWins.Keys.OrderBy(key => key))
            {
                Console.WriteLine("{0} - {1}", team, result.TeamWins[team]);
            }
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
