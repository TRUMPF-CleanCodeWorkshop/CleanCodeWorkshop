using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Contracts;
using Contracts.Model;

namespace GameEngine
{
    public class MyGameEngine : IGameEngine
    {
        private static readonly Random Randomizer = new Random();

        public GameState CreateInitializeGameState(GameConfiguration configuration, IEnumerable<IRobotEngine> robotEngines)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            var result = new GameState
            {
                Configuration = configuration,
                Turn = 0,
                PowerUps = new List<PowerUp>(),
                Robots = new List<Robot>()
            };

            var robotEngineList = robotEngines.ToList();
            var positions = GetInitialRobotPositions(configuration.MapSize, robotEngineList.Count).ToList();

            var robots = robotEngineList.Zip(positions, (engine, position) => new Robot(position, configuration.RobotStartLevel, engine.TeamName, engine.GetNewRobot())).ToList();

            result.Robots = robots;

            return result;
        }

        private static IEnumerable<Point> GetInitialRobotPositions(Size mapSize, int count)
        {
            var results = new List<Point>();

            for (var i = 0; i < count; i++)
            {
                Point nextPoint;
                do
                {
                    nextPoint = new Point(Randomizer.Next(mapSize.Width), Randomizer.Next(mapSize.Height));
                }
                while (results.Contains(nextPoint));
                results.Add(nextPoint);
            }

            return results;
        }

        public GameState CreateNextTurn(GameState gameState)
        {
            GeneratePowerUps(gameState);
            EvaluateRobots(gameState);

            return gameState;
        }

        public GameResults GetGameResult(GameState gameState)
        {
            return new GameResults();
        }

        private void EvaluateRobots(GameState gameState)
        {
            EvaluateSplitAndImrove(gameState);
            DecreaseWaitCounter(gameState);
            GetNextTurnsFromRobots(gameState);
            DoRobotMovements(gameState);
            ResolveFieldConflicts(gameState);
            DoPowerUps(gameState);
        }

        private void ResolveFieldConflicts(GameState gameState)
        {
            JoinRobots(gameState);
            FightRobots(gameState);
        }

        internal void FightRobots(GameState gameState)
        {
            throw new NotImplementedException();
        }

        internal void JoinRobots(GameState gameState)
        {
            throw new NotImplementedException();
        }

        private void DoPowerUps(GameState gameState)
        {
            throw new NotImplementedException();
        }

        private void DoRobotMovements(GameState gameState)
        {
            throw new NotImplementedException();
        }

        private void GetNextTurnsFromRobots(GameState gameState)
        {
            throw new NotImplementedException();
        }

        private void DecreaseWaitCounter(GameState gameState)
        {
            throw new NotImplementedException();
        }

        private void EvaluateSplitAndImrove(GameState gameState)
        {
            throw new NotImplementedException();
        }

        private void GeneratePowerUps(GameState gameState)
        {
            throw new NotImplementedException();
        }
    }
}
