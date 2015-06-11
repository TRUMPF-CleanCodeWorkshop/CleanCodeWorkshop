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
            // Next random direction
            var decisionDirection = (Directions)this.random.Next(0, 7);

            // Socializing
            if (!environment.Robots.Any(r => r.IsEnemy))
            {
                return new NextRobotTurn() { NextAction = RobotActions.Splitting, NextDirection = decisionDirection };
            }

            // Nimmersatt
            if (environment.PowerUps.Any())
            {
                var firstPowerUpDirection = environment.PowerUps.First().Direction;
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = firstPowerUpDirection };
            }

            // Hercules
            if (environment.Robots.Any(r => r.IsEnemy && r.Level < myLevel))
            {
                var strongestCatchableEnemyDirection = environment.Robots.Where(r => r.IsEnemy && r.Level < myLevel).OrderByDescending(r => r.Level).First().Direction;
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = strongestCatchableEnemyDirection };
            }

            // Darwin!
            var enemies = environment.Robots.Where(r => r.IsEnemy).ToList();
            var friends = environment.Robots.Where(r => !r.IsEnemy).ToList();

            foreach (var alliedRobot in friends)
            {
                foreach (var victim in enemies)
                {
                    var victimIsInNeigbourhood = RobotAreNeigbours(alliedRobot, victim);
                    if ((alliedRobot.Level + myLevel > victim.Level) && victimIsInNeigbourhood)
                    {
                        return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = victim.Direction };
                    }
                }
            }

            // yes I'm a pussy! fleee!!!!
            if (environment.Robots.Any(r => r.IsEnemy && r.Level >= myLevel))
            {
                var escapePlan = CalculateEscapePlan(environment.Robots.Where(r => r.IsEnemy).ToList());
                if (escapePlan.Exists)
                {
                    return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = escapePlan.Direction };
                }
            }

            // prepare for fight
            if (!environment.Robots.Any(r => r.IsEnemy && r.Level > myLevel))
            {
                return new NextRobotTurn() { NextAction = RobotActions.Upgrading, NextDirection = decisionDirection };
            }

            return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = decisionDirection };
        }

        private static EscapePlan CalculateEscapePlan(List<SurroundingRobot> enemies)
        {
            var strongestEnemy = enemies.First(r => r.Level.Equals(enemies.Max(m => m.Level)));
            var oppositeDirection = ((int)strongestEnemy.Direction + 4) % 7;
            if (!enemies.Any(r => r.Direction.Equals(oppositeDirection)))
            {
                return new EscapePlan()
                           {
                               Direction = (Directions)oppositeDirection,
                               Exists = true
                           };
            }
            var allFieldsAround = new List<Directions>()
                                      {
                                          Directions.N,
                                          Directions.NE,
                                          Directions.E,
                                          Directions.SE,
                                          Directions.S,
                                          Directions.SW,
                                          Directions.W,
                                          Directions.NW
                                      };

            var anyPlaceToHide = allFieldsAround.Except(enemies.Select(r => r.Direction)).ToList();
            if (anyPlaceToHide.Any())
            {
                return new EscapePlan()
                           {
                               Exists = true,
                               Direction = anyPlaceToHide.First()
                           };
            }
            return new EscapePlan(){Exists = false};
        }

        private static bool RobotAreNeigbours(SurroundingRobot robot1, SurroundingRobot robot2)
        {
            if (((int)robot1.Direction < 7) && ((int)robot1.Direction + 1 == (int)robot2.Direction))
            {
                return true;
            }

            if (((int)robot1.Direction > 0) && ((int)robot1.Direction - 1 == (int)robot2.Direction))
            {
                return true;
            }

            return false;
        }

        internal class EscapePlan
        {
            public bool Exists { get; set; }

            public Directions Direction { get; set; }

            public EscapePlan()
            {
                this.Direction = Directions.N;
            }
        }
    }
}
