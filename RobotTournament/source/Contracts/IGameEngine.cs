using GameEngine;

namespace Contracts
{
    using System.Collections.Generic;
    using System.Drawing;

    using Contracts.Model;

    public interface IGameEngine
    {
        GameState CreateInitializeGameState(GameConfiguration configuration, IEnumerable<IRobotEngine> robotEngines);

        GameState CreateNextTurn(GameState gameState);

        GameResults GetGameResult(GameState gameState);
    }
}
