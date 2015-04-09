using Contracts.Model;

namespace Contracts
{
    public class NextRobotTurn
    {
        public RobotActions NextAction { get; set; }
        public Directions NextDirection { get; set; }
    }
}