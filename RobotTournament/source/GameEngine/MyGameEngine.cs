using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using Contracts;
using Contracts.Model;

namespace GameEngine
{
    public class MyGameEngine : IGameEngine
    {
        private static readonly Random Randomizer = new Random();

        public GameState CreateInitializeGameState(GameConfiguration configuration, IEnumerable<IRobotEngine> robotEngines)
        {
            var result = new GameState
            {
                Configuration = configuration,
                Turn = 0,
                PowerUps = new List<PowerUp>(),
                Robots = new List<Robot>()
            };

            var robotEngineList = robotEngines.ToList();
            var positions = GetInitialRobotPositions(configuration.MapSize, robotEngineList.Count).ToList();

            var robots = robotEngineList.Zip(positions, (engine, position) => new Robot(position, configuration.RobotStartLevel, engine.TeamName, engine.GetNewRobot())).ToList();

            result.Robots = robots;

            return result;
        }

        private static IEnumerable<Point> GetInitialRobotPositions(Size mapSize, int count)
        {
            var results = new List<Point>();

            for (var i = 0; i < count; i++)
            {
                Point nextPoint;
                do
                {
                    nextPoint = new Point(Randomizer.Next(mapSize.Width), Randomizer.Next(mapSize.Height));
                }
                while (results.Contains(nextPoint));
                results.Add(nextPoint);
            }

            return results;
        }

        public GameState CreateNextTurn(GameState gameState)
        {
            GeneratePowerUps(gameState);
            EvaluateRobots(gameState);

            return gameState;
        }

        public GameResults GetGameResult(GameState gameState)
        {
            return new GameResults();
        }

        private void EvaluateRobots(GameState gameState)
        {
            EvaluateSplitAndImprove(gameState);
            DecreaseWaitCounter(gameState);
            GetNextTurnsFromRobots(gameState);
            DoRobotMovements(gameState);
            ResolveFieldConflicts(gameState);
            DoPowerUps(gameState);
        }

        private void ResolveFieldConflicts(GameState gameState)
        {
            JoinRobots(gameState);
            FightRobots(gameState);
        }

        internal void FightRobots(GameState gameState)
        {
            var groupedByPos = gameState.Robots.ToList().GroupBy(r => r.Position);
            foreach (var group in groupedByPos.Where(g => g.Count() > 1))
            {
                this.FightRobotsOnSameField(gameState, group);
            }
        }

        internal void FightRobotsOnSameField(GameState state, IEnumerable<Robot> robots)
        {
            var robotList = robots.ToList();
            var sortedRobotList = robotList.OrderByDescending(r => r.Level).ToList();
            var winner = sortedRobotList.First();

            robotList.ToList().ForEach(r => state.Robots.Remove(r));

            if (winner.Level != sortedRobotList[1].Level)
            {
                state.Robots.Add(winner);
            }
        }

        internal void JoinRobots(GameState gameState)
        {
            var groupedByPos = gameState.Robots.ToList().GroupBy(r => r.Position);
            foreach (var group in groupedByPos.Where(g => g.Count() > 1))
            {
                this.JoinRobotsOnSameField(gameState, group.Key, group);
            }
        }

        internal void JoinRobotsOnSameField(GameState state, Point pos, IEnumerable<Robot> robots)
        {
            var groupedByTeam = robots.GroupBy(r => r.TeamName);
            foreach (var team in groupedByTeam.Where(g => g.Count() > 1))
            {
                var joinedBot = new Robot(
                    pos,
                    team.Sum(r => r.Level),
                    team.Key,
                    team.First().RobotImplementation);
                team.ToList().ForEach(r => state.Robots.Remove(r));
                state.Robots.Add(joinedBot);
            }
        }

        private void DoPowerUps(GameState gameState)
        {
            throw new NotImplementedException();
        }

        private void DoRobotMovements(GameState gameState)
        {
            throw new NotImplementedException();
        }

        internal void GetNextTurnsFromRobots(GameState gameState)
        {
            var readyRobots = gameState.Robots.Where(robot => robot.WaitTurns == 0).ToList();

            foreach (var robot in readyRobots)
            {
                DoNextTurnForRobot(gameState, robot);
            }

        }

        private static void DoNextTurnForRobot(GameState gameState, Robot robot)
        {
            var surroundings = new Surroundings();
            surroundings.Robots = GetSurroundingRobots(gameState, robot);

            var result = robot.RobotImplementation.DoNextTurn(gameState.Turn, robot.Level, surroundings);

            robot.CurrentAction = result.NextAction;
            robot.CurrentDirection = result.NextDirection;

            if (robot.CurrentAction == RobotActions.Splitting || robot.CurrentAction == RobotActions.Upgrading)
            {
                robot.WaitTurns = 2;
            }
        }

        internal void DecreaseWaitCounter(GameState gameState)
        {
            gameState.Robots.Where(r => r.WaitTurns >= 1).ToList().ForEach(r => r.WaitTurns -= 1);
        }

        internal void EvaluateSplitAndImprove(GameState gameState)
        {
            var robots = gameState.Robots.Where(r => r.WaitTurns == 1).ToList();

            var splitRobots = robots.Where(r => r.CurrentAction.Equals(RobotActions.Splitting)).ToList();
            var improveRobots = robots.Where(r => r.CurrentAction.Equals(RobotActions.Upgrading)).ToList();

            splitRobots.ForEach(r => PerformSplit(r, gameState));
            improveRobots.ForEach(PerformImprove);
        }

        internal void PerformImprove(Robot robot)
        {
            robot.Level += 2;
        }

        internal void PerformSplit(Robot robot, GameState gameState)
        {
            // TODO Copy robot to given position

            // TODO Decrease strength of both robots (half)
        }

        private void GeneratePowerUps(GameState gameState)
        {
            var powerup = new PowerUp();
            var propability = gameState.Configuration.PowerupPropability;
            var mapSize = gameState.Configuration.MapSize;
            var powerups = new List<PowerUp>();
            var count = Math.Round(mapSize.Width * mapSize.Height * propability, 0);

            for (var i = 0; i < count; i++)
            {
                Point nextPoint;
                do
                {
                    nextPoint = new Point(Randomizer.Next(mapSize.Width), Randomizer.Next(mapSize.Height));
                }
                while (powerups.Select(p => p.Position).Contains(nextPoint));
                powerup.Position = nextPoint;

                powerups.Add(powerup);
            }
        }

        private static IEnumerable<SurroundingRobot> GetSurroundingRobots(GameState gameState, Robot robot)
        {
            var surroundingPositions = GetSurroundingPositions(robot.Position);

            var robots = gameState.Robots.Where(r => surroundingPositions.Contains(r.Position));

            return robots.Select(r => new SurroundingRobot() { Level = r.Level, IsEnemy = r.TeamName != robot.TeamName, Direction = Directions.N }).ToList();
        }

        internal static Directions GetDirectionFromRelativePositions(Point currentRobotPosition, Point surroundRobotPosition)
        {
            var relativePosition = surroundRobotPosition.Substract(currentRobotPosition);

            if (relativePosition.Equals(new Point(-1, -1))) { return Directions.NW; }
            if (relativePosition.Equals(new Point(0, -1))) { return Directions.N; }
            if (relativePosition.Equals(new Point(1, -1))) { return Directions.NE; }
            if (relativePosition.Equals(new Point(-1, 0))) { return Directions.W; }
            if (relativePosition.Equals(new Point(1, 0))) { return Directions.E; }
            if (relativePosition.Equals(new Point(-1, 1))) { return Directions.SW; }
            if (relativePosition.Equals(new Point(0, 1))) { return Directions.S; }
            if (relativePosition.Equals(new Point(1, 1))) { return Directions.SE; }

            throw new Exception(string.Format("Invalid direction with robot {0} and surrounding robot {1}", currentRobotPosition, surroundRobotPosition));
        }

        private static IEnumerable<Point> GetSurroundingPositions(Point position)
        {
            yield return new Point(position.X - 1, position.Y - 1);
            yield return new Point(position.X, position.Y - 1);
            yield return new Point(position.X + 1, position.Y - 1);
            yield return new Point(position.X - 1, position.Y);
            yield return new Point(position.X + 1, position.Y);
            yield return new Point(position.X - 1, position.Y + 1);
            yield return new Point(position.X, position.Y + 1);
            yield return new Point(position.X + 1, position.Y + 1);
        }
    }
}
