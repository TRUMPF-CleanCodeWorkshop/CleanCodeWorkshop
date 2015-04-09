namespace Contracts
{
    using System.Collections.Generic;
    using System.Drawing;

    using Contracts.Model;

    public interface IGameEngine
    {
        GameState CreateNextTurn(GameState gameState);

        IEnumerable<Point> GetInitialRobotPositions(Size mapSize, int count); 
    }
}
