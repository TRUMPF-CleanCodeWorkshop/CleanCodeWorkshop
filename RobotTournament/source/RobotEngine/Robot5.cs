namespace RobotEngine
{
    using System;
    using System.Linq;

    using Contracts;
    using Contracts.Model;

    public class Robot5 : IRobot
    {
        public NextRobotTurn DoNextTurn(int currentTurn, int myLevel, Surroundings environment)
        {
            if (currentTurn == 0)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Splitting, NextDirection = Directions.NE };
            }

            var surroundingRobots = environment.Robots;
            var surroundingPowerUps = environment.PowerUps;

            if (surroundingPowerUps.Any())
            {
                var maxPowerUp = surroundingPowerUps.OrderBy(p => p.Level).First();
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = maxPowerUp.Direction };
            }

            var enemies = surroundingRobots.Where(r => r.IsEnemy).ToList();

            if (!enemies.Any())
            {
                if (myLevel > 5 || myLevel % 2 == 1)
                {
                    return new NextRobotTurn()
                    {
                        NextAction = RobotActions.Splitting,
                        NextDirection = this.GetRandomDirections()
                    };
                }
                if (myLevel % 2 == 0)
                {
                    return new NextRobotTurn() { NextAction = RobotActions.Upgrading };
                }
            }

            var strongerEnemies = enemies.Where(e => e.Level >= myLevel).ToList();

            if (strongerEnemies.Any())
            {
                var randomDirection = this.GetRandomDirections();
                var counter = 0;
                while (strongerEnemies.Where(se => se.Direction == randomDirection).ToList().Any() && counter++ < 20)
                {
                    randomDirection = this.GetRandomDirections();
                }

                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = randomDirection };
            }

            return new NextRobotTurn() { NextAction = RobotActions.Upgrading };
        }

        internal Directions GetRandomDirections()
        {
            return (Directions)new Random().Next(7);
        }
    }
}
