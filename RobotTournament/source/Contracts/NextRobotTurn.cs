using Contracts.Model;

namespace Contracts
{
    public class NextRobotTurn
    {
        RobotActions NextAction { get; set; }
        Directions NextDirection { get; set; }
    }
}