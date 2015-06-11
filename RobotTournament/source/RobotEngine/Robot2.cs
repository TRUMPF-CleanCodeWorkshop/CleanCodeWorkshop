namespace RobotEngine
{
    using System;
    using System.Linq;

    using Contracts;
    using Contracts.Model;

    public class Robot2:IRobot
    {
        public NextRobotTurn DoNextTurn(int currentTurn, int myLevel, Surroundings environment)
        {
            if (environment.PowerUps.Any())
            {
                var firstPowerUpDirection = environment.PowerUps.First().Direction;
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = firstPowerUpDirection };
            }

            if (environment.Robots.Any(r => r.IsEnemy && r.Level < myLevel))
            {
                var firstPussyBot = environment.Robots.First(r => r.IsEnemy && r.Level < myLevel).Direction;
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = firstPussyBot };
            }

            var decisionDirection = currentTurn % 5 == 0 ? Directions.NW : Directions.SW;

            if (currentTurn % 3 == 0)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Splitting, NextDirection = decisionDirection };
            }

            return currentTurn % 7 == 0 ? new NextRobotTurn() { NextAction = RobotActions.Upgrading, NextDirection = decisionDirection } : new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = decisionDirection };
        }
    }
}
