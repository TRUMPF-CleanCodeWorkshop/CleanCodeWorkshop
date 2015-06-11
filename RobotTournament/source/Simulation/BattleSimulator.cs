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
        public SimulationResult Simulate(GameConfiguration configuration, IGameEngine gameEngine, IEnumerable<IRobotEngine> robotEngines)
        {
            var gameState = gameEngine.CreateInitializeGameState(configuration, robotEngines);

            while (!gameState.Finished)
            {
                gameState = gameEngine.CreateNextTurn(gameState);
            }

            var teamWins = new Dictionary<string, int>();

            if (gameState.Robots.Count == 0)
            {
                teamWins.Add("None",1);
            }
            else
            {
                teamWins.Add(gameState.Robots.First().TeamName, 1);    
            }

            return new SimulationResult() {TeamWins = teamWins};
        }
    }
}
