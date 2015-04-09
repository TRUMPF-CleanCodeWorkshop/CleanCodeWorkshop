using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Model;

namespace Core
{
    public class Framework
    {
        public static GameState CreateInitializeGameState(GameConfiguration configuration, IGameEngine engine, IEnumerable<IRobotEngine> robotEngines)
        {
            return new GameState();
        } 
    }
}
