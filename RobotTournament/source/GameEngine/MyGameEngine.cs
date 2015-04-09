using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Model;

namespace GameEngine
{
    public class MyGameEngine: IGameEngine
    {
        public GameState CreateNextTurn(GameState gameState)
        {
            throw new NotImplementedException();
        }
    }
}
