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
        public SimulationResult Simulate(IGameEngine gameEngine, IEnumerable<IRobotEngine> robotEngines)
        {
            return new SimulationResult();
        }
    }
}
