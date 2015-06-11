namespace RobotEngine
{
    using System;
    using System.Linq;
    using Contracts;
    using Contracts.Model;

    internal class Robot3 : IRobot
    {

        public NextRobotTurn DoNextTurn(int currentTurn, int myLevel, Surroundings environment)
        {
            if (!environment.Robots.Any(r => r.Level >= myLevel && r.IsEnemy) && environment.PowerUps.Any())
            {
                var firstPowerUpDirection = environment.PowerUps.First().Direction;
                return new NextRobotTurn {NextAction = RobotActions.Moving, NextDirection = firstPowerUpDirection};
            }

            if (currentTurn <= 6 && !environment.Robots.Any(r => r.Level > myLevel && r.IsEnemy))
            {
                return new NextRobotTurn
                {
                    NextAction = RobotActions.Upgrading, 
                    NextDirection = Directions.W
                };
            }

            if (environment.Robots.Any(r => r.IsEnemy && r.Level >= Math.Round(myLevel / 2.0, MidpointRounding.AwayFromZero)) 
                || !environment.Robots.Any())
            {
                return new NextRobotTurn
                {
                    NextAction = RobotActions.Splitting,
                    NextDirection = Directions.W
                };
            }

            if (!environment.Robots.Any(r => r.IsEnemy))
            {
                var random = new Random();
                var randomDirection = (Directions)random.Next(0, 8);
                return new NextRobotTurn
                {
                    NextAction = RobotActions.Moving, 
                    NextDirection = randomDirection
                };
            }

            // Schwacher Robot in Reichweite
            if (environment.Robots.Any(r => r.IsEnemy && r.Level < myLevel))
            {
                return new NextRobotTurn
                {
                    NextAction = RobotActions.Moving, 
                    NextDirection = environment.Robots.First(r => r.IsEnemy && r.Level < myLevel).Direction
                };
            }

            if (environment.Robots.Any(r => r.IsEnemy && r.Level > myLevel))
            {
                Directions directionOfFirstDangerousBot =
                    environment.Robots.First(r => r.IsEnemy && r.Level > myLevel).Direction;
                Directions directionToMove = directionOfFirstDangerousBot + 4;
                if ((int) directionToMove > 7)
                {
                    directionToMove -= 7;
                }

                return new NextRobotTurn {NextAction = RobotActions.Moving, NextDirection = directionToMove};
            }

            var random2 = new Random();
            var randomDirection2 = (Directions) random2.Next(0, 8);
            return new NextRobotTurn
            {
                NextAction = RobotActions.Moving, 
                NextDirection = randomDirection2
            };
        }
    }
}