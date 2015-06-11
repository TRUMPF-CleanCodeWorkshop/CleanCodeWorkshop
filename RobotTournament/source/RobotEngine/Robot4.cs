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

            //Nur für Erste Runde
            if (currentTurn == 0)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Splitting, NextDirection = decisionDirection };
            }

            
            if (environment.Robots.Any(r => r.IsEnemy && r.Level > myLevel))
            {
                //Lauf Forest!!!
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = decisionDirection };
            }
            else if (environment.PowerUps.Any() == false && myLevel < 4)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Upgrading, NextDirection = decisionDirection };
            }
            else if (environment.PowerUps.Any())
            {
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = environment.PowerUps.First().Direction };
            }
                /*
            else if (myLevel >= 20 && friends.Any(r => r.Level >= 20))
            {
                //geht noch nicht richtig, hier soll ein Merge gemacht werden
                var merger = friends.First(r => r.Level >= 20).Direction;
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = merger };
            }*/
            else if (myLevel >= 4)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Splitting, NextDirection = decisionDirection };
            }
            else if (environment.Robots.Any(r => r.IsEnemy && r.Level < myLevel))
            {
                //Fight, weil ich stärker bin
                var looser = environment.Robots.First(r => r.IsEnemy && r.Level < myLevel).Direction;
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = looser };
            }
            else
            {
                return new NextRobotTurn() { NextAction = RobotActions.Upgrading, NextDirection = decisionDirection };
            }
        }
    }
}
