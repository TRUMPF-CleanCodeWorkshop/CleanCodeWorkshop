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
            // Condition variables
            var noDangerAndPowerUpsToGet = !environment.Robots.Any(r => r.Level >= myLevel && r.IsEnemy) &&
                                               environment.PowerUps.Any();
            var noDangerAndBeginningOfGame = currentTurn <= 6 &&
                                             !environment.Robots.Any(r => r.Level > myLevel && r.IsEnemy);
            var dangerousEnemyAround = environment.Robots.Any(r => r.IsEnemy && r.Level > myLevel);
            var weakEnemyAround = environment.Robots.Any(r => r.IsEnemy && r.Level < myLevel);
            var enemyAround = environment.Robots.Any(r => r.IsEnemy);
            var noRobotsOrNoDangerousRobotsAroundAndMyLevelRelativelyHigh = (environment.Robots.Any(
                r => r.IsEnemy && r.Level >= Math.Round(myLevel / 2.0, MidpointRounding.AwayFromZero))
                               || !environment.Robots.Any()) 
                               && myLevel > (currentTurn / 2);

            if (noDangerAndPowerUpsToGet)
            {
                var firstPowerUpDirection = environment.PowerUps.First().Direction;
                return new NextRobotTurn {NextAction = RobotActions.Moving, NextDirection = firstPowerUpDirection};
            }

            if (noDangerAndBeginningOfGame)
            {
                return new NextRobotTurn
                {
                    NextAction = RobotActions.Upgrading, 
                    NextDirection = Directions.W
                };
            }

            if (noRobotsOrNoDangerousRobotsAroundAndMyLevelRelativelyHigh)
            {
                return new NextRobotTurn
                {
                    NextAction = RobotActions.Splitting,
                    NextDirection = Directions.W
                };
            }

            if (!enemyAround)
            {
                var random = new Random();
                var randomDirection = (Directions)random.Next(0, 8);
                return new NextRobotTurn
                {
                    NextAction = RobotActions.Moving, 
                    NextDirection = randomDirection
                };
            }

            if (weakEnemyAround)
            {
                return new NextRobotTurn
                {
                    NextAction = RobotActions.Moving, 
                    NextDirection = environment.Robots.First(r => r.IsEnemy && r.Level < myLevel).Direction
                };
            }

            if (dangerousEnemyAround)
            {
                var directionOfFirstDangerousBot =
                    environment.Robots.First(r => r.IsEnemy && r.Level > myLevel).Direction;
                var directionToMove = directionOfFirstDangerousBot + 4;
                if ((int)directionToMove > 7)
                {
                    directionToMove -= 8;
                }

                return new NextRobotTurn {NextAction = RobotActions.Moving, NextDirection = directionToMove};
            }

            var random2 = new Random();
            var randomDirection2 = (Directions)random2.Next(0, 8);
            return new NextRobotTurn
            {
                NextAction = RobotActions.Moving, 
                NextDirection = randomDirection2
            };
        }
    }
}