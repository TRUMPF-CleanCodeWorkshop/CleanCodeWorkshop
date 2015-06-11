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
            if (currentTurn == 0 && environment.Robots.Any(r => r.IsEnemy) && (EscapeDirection(environment.Robots) != null))
            {
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = EscapeDirection(environment.Robots) };
            }

            var decisionDirection = (Directions)this.random.Next(0, 7);
            
            if (environment.Robots.Count(r => !r.IsEnemy) == 8)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Upgrading, NextDirection = decisionDirection };
            }

            if (environment.PowerUps.Any() && !environment.PowerUps
                .Any(p => environment.Robots.Any(r => 
                    r.Level >= myLevel
                    && r.IsEnemy
                    && IsNextTo(r.Direction, p.Direction))))
            {
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = environment.PowerUps.First().Direction };
            }

            if (environment.Robots.Any(r => r.IsEnemy && (r.Level > myLevel)) && environment.Robots.Any(r => !r.IsEnemy))
            {
                var friendsAroundMe = environment.Robots.Where(r => !r.IsEnemy).ToList();
                var bigBuddy = friendsAroundMe.First(r => r.Level.Equals(friendsAroundMe.Min(b => b.Level)));
                var escapeDirection = bigBuddy.Direction;
                
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = escapeDirection };
            }

            return new NextRobotTurn() { NextAction = RobotActions.Splitting, NextDirection = decisionDirection };
        }

        public static bool IsNextTo(Directions firstDirection, Directions secondDirection)
        {
            var previousDirection = ((int)firstDirection - 1) % 7;
            var nextDirection = ((int)firstDirection + 1) % 7;

            return (nextDirection == (int)secondDirection) || (previousDirection == (int)secondDirection);
        }

        public static Directions EscapeDirection(IEnumerable<SurroundingRobot> enemies)
        {
            var safePlaces = new List<Directions>();

            foreach (var enemy in enemies)
            {
                var oppositeSide = ((int)enemy.Direction + 4) % 7;
                if (!enemies.Any(e => e.Direction.Equals(oppositeSide)))
                {
                    safePlaces.Add((Directions)oppositeSide);
                }
            }

            var steps = (int)Math.Round((double)safePlaces.Count() / 2);
            return safePlaces.Skip(steps).Take(1).First();
        }
    }
}
