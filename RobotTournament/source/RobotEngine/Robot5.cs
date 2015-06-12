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
            var splittingLimit = currentTurn < 20 ? 8 : currentTurn / 2;
            
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

                var randomDirection = this.GetRandomDirections();
                var counter = 0;
                while (strongerEnemies.Where(se => se.Direction == randomDirection).ToList().Any() && counter++ < 20)
                {
                    randomDirection = this.GetRandomDirections();
                }

                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = randomDirection };
            }

            if (surroundingPowerUps.Any())
            {
                var maxPowerUp = surroundingPowerUps.OrderBy(p => p.Level).First();
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = maxPowerUp.Direction };
            }
            
            if (myLevel >= splittingLimit)
            {
                return new NextRobotTurn()
                {
                    NextAction = RobotActions.Splitting,
                    NextDirection = this.GetRandomDirections()
                };
            }

            return new NextRobotTurn() { NextAction = RobotActions.Upgrading };
        }

        internal Directions GetRandomDirections()
        {
            return (Directions)new Random().Next(7);
        }
    }
}
