namespace Contracts
{
    public interface IRobotEngine
    {
        string TeamName { get; }
        IRobot GetNewRobot();
    }
}