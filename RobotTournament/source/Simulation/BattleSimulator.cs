using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Model;

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

        private static void ApplyWinngTeam(SimulationResult result, string winningTeam)
        {
            Console.WriteLine("Finished: " + winningTeam);
            lock (result)
            {
                if (result.TeamWins.ContainsKey(winningTeam))
                {
                    result.TeamWins[winningTeam] += 1;
                }
                else
                {
                    result.TeamWins.Add(winningTeam, 1);
                }
            }
        }

        private static string SimulateOne(GameConfiguration configuration, IGameEngine gameEngine, IEnumerable<IRobotEngine> robotEngines)
        {
            var gameState = gameEngine.CreateInitializeGameState(configuration, robotEngines);

            while (!gameState.Finished)
            {
                gameState = gameEngine.CreateNextTurn(gameState);
            }

            return gameState.Robots.Count == 0 ? "None" : gameState.Robots.First().TeamName;
        }
    }
}
