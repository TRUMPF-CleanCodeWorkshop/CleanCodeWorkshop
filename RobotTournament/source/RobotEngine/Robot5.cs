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
            var splittingLimit = currentTurn < 25 ? 8 : currentTurn * 2 / 3;

            var attackLimit = currentTurn < 200 ? currentTurn * 2 : (int)(currentTurn * 1.4);
            
            var surroundingRobots = environment.Robots;
            var surroundingPowerUps = environment.PowerUps;
            var enemies = surroundingRobots.Where(r => r.IsEnemy).ToList();
            var friends = surroundingRobots.Where(r => !r.IsEnemy).ToList();

            if (currentTurn < 5)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Splitting, NextDirection = this.GetRandomDirections() };
            }

            var strongerEnemies = enemies.Where(e => e.Level >= myLevel).ToList();

            if (strongerEnemies.Any())
            {
                if (friends.Any())
                {
                    var strongestFriendAround = friends.OrderByDescending(f => f.Level).First();
                    return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = strongestFriendAround.Direction };
                }

                var strongestEnemyAround = strongerEnemies.OrderByDescending(e => e.Level).First();

                var robotDirection = this.GetOppositeDirection(strongestEnemyAround.Direction);
                var counter = 0;
                while (strongerEnemies.Where(se => se.Direction == robotDirection).ToList().Any() && counter++ < 20)
                {
                    robotDirection = this.GetRandomDirections();
                }

                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = robotDirection };
            }

            if (surroundingPowerUps.Any())
            {
                var maxPowerUp = surroundingPowerUps.OrderByDescending(p => p.Level).First();
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = maxPowerUp.Direction };
            }

            if (currentTurn > 30 && myLevel > attackLimit && new Random().Next(10) > 6)
            {
                var significantlyWeakerEnemies = enemies.Where(e => e.Level < myLevel * 0.8).ToList();
                if (significantlyWeakerEnemies.Any())
                {
                    var strongestWeakerEnemy = significantlyWeakerEnemies.OrderByDescending(e => e.Level).First();
                    return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = strongestWeakerEnemy.Direction };
                }
            }

            if (myLevel >= splittingLimit)
            {
                var robotDirection = this.GetRandomDirections();

                var counter = 0;
                while (friends.Any(f => f.Direction == robotDirection) && counter++ < 20)
                {
                    robotDirection = this.GetRandomDirections();
                }

                return new NextRobotTurn()
                {
                    NextAction = RobotActions.Splitting,
                    NextDirection = robotDirection
                };
            }

            return new NextRobotTurn() { NextAction = RobotActions.Upgrading };
        }

        internal Directions GetRandomDirections()
        {
            return (Directions)new Random().Next(7);
        }

        internal Directions GetOppositeDirection(Directions direction)
        {
            switch (direction)
            {
                case Directions.N:
                    return Directions.S;
                case Directions.NE:
                    return Directions.SW;
                case Directions.E:
                    return Directions.W;
                case Directions.SE:
                    return Directions.NW;
                case Directions.S:
                    return Directions.N;
                case Directions.SW:
                    return Directions.NE;
                case Directions.W:
                    return Directions.E;
                default:
                    return Directions.SE;
            }
        }
    }
}
