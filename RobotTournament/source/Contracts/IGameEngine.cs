using Contracts.Model;

namespace Contracts
{
    public interface IGameEngine
    {
        GameState CreateNextTurn(GameState gameState);
    }
}
