using System.Drawing;

namespace Contracts
{
    public interface IRobotEngine
    {
        string TeamName { get; }
        IRobot GetNewRobot();
        Color GetTeamColor();
    }
}