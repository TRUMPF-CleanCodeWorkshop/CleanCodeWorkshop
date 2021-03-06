﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Contracts.Model;
using DebugUI;
using GameEngineAdapter;
using Host.Properties;
using RobotEngineAdapter;

namespace Host
{
    using System.Linq;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var basePath = GetBasePath();
            var gameEnginePath =  Path.Combine(basePath , "engines");
            var robotEnginePath = Path.Combine(basePath, "robots");

            var configuration = new GameConfiguration() {MapSize = Settings.Default.MapSize, PowerupPropability = Settings.Default.PowerUpPropability, RobotStartLevel = Settings.Default.RobotStartLevel, MaxTurns = Settings.Default.MaxTurns};
            var gameEngine = EngineLoader.Load(gameEnginePath);
            var robotEngines = RobotLoader.Load(robotEnginePath).ToList();
            var gameState = gameEngine.CreateInitializeGameState(configuration, robotEngines);
            
            GameUI.ShowGameState(gameState);

            Console.ReadLine();

            while (!gameState.Finished)
            {
                gameState = gameEngine.CreateNextTurn(gameState);
                GameUI.ShowGameState(gameState);
                Console.ReadLine();
            }
        }

        private static string GetBasePath()
        {
            var file = new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            // ReSharper disable once PossibleNullReferenceException
            return file.Directory.FullName;
        }
    }
}
