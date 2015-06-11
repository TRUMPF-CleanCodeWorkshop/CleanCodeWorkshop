namespace RobotEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Contracts.Model;

    public class Robot2:IRobot
    {
        private Random random = new Random();

        public NextRobotTurn DoNextTurn(int currentTurn, int myLevel, Surroundings environment)
        {
            // ROBOTNIK REMASTERED!
            var decisionDirection = (Directions)this.random.Next(0, 7);

            return environment.Robots.Count(r => !r.IsEnemy) == 8 ? 
                new NextRobotTurn() { NextAction = RobotActions.Upgrading, NextDirection = decisionDirection } : 
                new NextRobotTurn() { NextAction = RobotActions.Splitting, NextDirection = decisionDirection };
        }
    }
}
