namespace RobotEngine.TheSwarm.Helper
{
    using Contracts;
    using Contracts.Model;

    public static class Actions
    {
        public static NextRobotTurn Move(Directions direction)
        {
            return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = direction };
        }

        public static NextRobotTurn Split(Directions direction)
        {
            return new NextRobotTurn() { NextAction = RobotActions.Splitting, NextDirection = direction };
        }

        public static NextRobotTurn Upgrade()
        {
            return new NextRobotTurn() { NextAction = RobotActions.Upgrading };
        }

        public static NextRobotTurn Idle()
        {
            return new NextRobotTurn() { NextAction = RobotActions.Idle };
        }
    }
}
