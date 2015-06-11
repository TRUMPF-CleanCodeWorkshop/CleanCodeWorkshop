namespace RobotEngine
{
    using System;
    using System.Linq;

    using Contracts;
    using Contracts.Model;

    public class Robot4 : IRobot
    {
        private Random random = new Random();

        public NextRobotTurn DoNextTurn(int currentTurn, int myLevel, Surroundings environment)
        {
            var enemies = environment.Robots.Where(r => r.IsEnemy).ToList();
            var friends = environment.Robots.Where(r => !r.IsEnemy).ToList();
             
            var decisionDirection = (Directions)random.Next(0, 7);
            if (currentTurn == 0)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Splitting, NextDirection = decisionDirection };
            }

            if (environment.Robots.Any(r => r.IsEnemy && r.Level < myLevel))
            {
                //Fight, weil ich stärker bin
                var looser = environment.Robots.First(r => r.IsEnemy && r.Level < myLevel).Direction;
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = looser };
            }
            else if (environment.Robots.Any(r => r.IsEnemy && r.Level > myLevel))
            {
                //Lauf Forest!!!
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = decisionDirection };
            }
            else if (environment.PowerUps.Any() == false && myLevel < 5)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Upgrading, NextDirection = decisionDirection };
            }
            else if (environment.PowerUps.Any())
            {
                //Wenn PowerUp dann friss es
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = environment.PowerUps.First().Direction };
            }
            else if (myLevel >= 5)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Splitting, NextDirection = decisionDirection };
            }
            else
            {
                return new NextRobotTurn() { NextAction = RobotActions.Upgrading, NextDirection = decisionDirection };
            }
        }
    }
}
