namespace RobotEngine.TheSwarm.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Contracts.Model;

    public static class SurroundingsExtensions
    {
        public static SurroundingRobot EnemyAtPos(this Surroundings surroundings, Directions direction)
        {
            return surroundings.Robots.SingleOrDefault(r => r.Direction == direction && r.IsEnemy);
        }

        public static SurroundingRobot FriendAtPos(this Surroundings surroundings, Directions direction)
        {
            return surroundings.Robots.SingleOrDefault(r => r.Direction == direction && !r.IsEnemy);
        }

        public static SurroundingRobot GetWeakestFriend(this Surroundings surroundings)
        {
            SurroundingRobot result = null;
            foreach (var enemy in surroundings.Robots.Where(r => !r.IsEnemy))
            {
                if (result == null || result.Level < enemy.Level)
                {
                    result = enemy;
                }
            }

            return result;
        }

        public static SurroundingRobot GetWeakestEnemy(this Surroundings surroundings)
        {
            SurroundingRobot result = null;
            foreach (var enemy in surroundings.Robots.Where(r => r.IsEnemy))
            {
                if (result == null || result.Level < enemy.Level)
                {
                    result = enemy;
                }
            }

            return result;
        }

        public static SurroundingPowerUp GetBestPowerUp(this Surroundings environment)
        {
            SurroundingPowerUp result = null;
            foreach (var powerUp in environment.PowerUps)
            {
                if (result == null || result.Level > powerUp.Level)
                {
                    result = powerUp;
                }
            }

            return result;
        }

        public static IEnumerable<Directions> GetSafeDirections(this Surroundings environment, int myLevel)
        {
            var allDirections = ((Directions[])Enum.GetValues(typeof(Directions))).ToList();
            foreach (var enemy in environment.Robots.Where(r => r.IsEnemy && r.Level >= myLevel))
            {
                allDirections.Remove(enemy.Direction);
                var reachableFromEnemy = enemy.Direction.GetReachableDirections().ToList();
                allDirections.RemoveAll(reachableFromEnemy.Contains);
            }

            return allDirections;
        }

        public static Directions BestSplitDirection(this Surroundings environment, int myLevel)
        {
            var allDirections = ((Directions[])Enum.GetValues(typeof(Directions))).ToList();
            var nonEmptySpaces = environment.Robots.Select(r => r.Direction).Distinct();
            allDirections.RemoveAll(nonEmptySpaces.Contains);
            if (allDirections.Any())
            {
                return allDirections.First();
            }

            var friend = environment.GetWeakestFriend();
            return friend != null ? friend.Direction : Directions.NE;
        }

        public static IEnumerable<SurroundingRobot> EnemiesWhoCanReach(this Surroundings environment, Directions direction)
        {
            var reachableFromTarget = direction.GetReachableDirections();
            return reachableFromTarget
                .Select(dir => environment.Robots.SingleOrDefault(r => r.Direction == dir && r.IsEnemy))
                .Where(botAtSpot => botAtSpot != null);
        }

        public static IEnumerable<SurroundingRobot> ThreatenedFriendsIMustHelp(this Surroundings environment, int myLevel)
        {
            var friends = environment.Robots.Where(r => !r.IsEnemy && r.Level < myLevel);
            foreach (var friend in friends)
            {
                var spotsAround = friend.Direction.GetReachableDirections();
                var enemiesAroundLevel = spotsAround.SelectMany(environment.EnemiesWhoCanReach).Sum(r => r.Level);
                if (enemiesAroundLevel < (friend.Level + myLevel))
                {
                    yield return friend;
                }
            }
        } 
    }
}
