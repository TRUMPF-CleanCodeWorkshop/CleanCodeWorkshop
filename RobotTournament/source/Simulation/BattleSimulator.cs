using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Contracts.Model;
using GameEngine;

namespace Simulation
{
    public class BattleSimulator
    {
        public static SimulationResult Simulate(GameConfiguration configuration, IGameEngine gameEngine, IEnumerable<IRobotEngine> robotEngines, int count)
        {
            var result = new SimulationResult() { TeamWins = new Dictionary<string, int>() };
            var options = new ParallelOptions() { MaxDegreeOfParallelism = 20 };

            Parallel.For(0, count, options, (counter) =>
            {
                var winningTeam = SimulateOne(configuration, gameEngine, robotEngines);
                ApplyWinngTeam(result, winningTeam);
            });

            return result;
        }

        private static void ApplyWinngTeam(SimulationResult result, GameResults gameResults)
        {
            Console.WriteLine("Finished: {0} after {1} turns" , gameResults.Winner, gameResults.RoundsTaken);
            lock (result)
            {
                if (result.TeamWins.ContainsKey(gameResults.Winner))
                {
                    result.TeamWins[gameResults.Winner] += 1;
                }
                else
                {
                    result.TeamWins.Add(gameResults.Winner, 1);
                }
            }
        }

        private static GameResults SimulateOne(GameConfiguration configuration, IGameEngine gameEngine, IEnumerable<IRobotEngine> robotEngines)
        {
            var gameState = gameEngine.CreateInitializeGameState(configuration, robotEngines);

            while (!gameState.Finished)
            {
                gameState = gameEngine.CreateNextTurn(gameState);
            }

            return gameState.GameResults;
        }
    }
}
