namespace GameEngine
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using Contracts;
    using Contracts.Model;

    public class MyGameEngine : IGameEngine
    {
        private static readonly Random Randomizer = new Random();

        public GameState CreateInitializeGameState(GameConfiguration configuration, IEnumerable<IRobotEngine> robotEngines)
        {
            var gameState = new GameState
            {
                Configuration = configuration,
                Turn = 0,
                PowerUps = new List<PowerUp>(),
                Robots = new List<Robot>(),
                TeamColors = robotEngines.ToDictionary(engine => engine.TeamName, engine => engine.GetTeamColor())
            };

            var robotEngineList = robotEngines.ToList();
            var positions = GetInitialRobotPositions(configuration.MapSize, robotEngineList.Count).ToList();
            var robots = robotEngineList.Zip(positions, (engine, position) => new Robot(position, configuration.RobotStartLevel, engine.TeamName, engine.GetNewRobot())).ToList();

            gameState.Robots = robots;

            return gameState;
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
            DecreaseWaitCounter(gameState);
            EvaluateSplitAndImprove(gameState);
            GetNextTurnsFromRobots(gameState);
            DoRobotMovements(gameState);
            ResolveFieldConflicts(gameState);
            DoPowerUps(gameState);
            IncreaseTurnOrEndGame(gameState);
        }

        internal void IncreaseTurnOrEndGame(GameState gameState)
        {
            if (GetTeamsAlive(gameState).Count > 1 && gameState.Turn < gameState.Configuration.MaxTurns)
            {
                IncreaseTurn(gameState);
            }
            else
            {
                FinalizeGame(gameState);
            }
        }

        private void IncreaseTurn(GameState gameState)
        {
            gameState.Turn += 1;
        }

        private void FinalizeGame(GameState gameState)
        {
            var remainingRobots = gameState.Robots.ToList();

            if (remainingRobots.Count > 0 && gameState.Turn < gameState.Configuration.MaxTurns)
            {
                gameState.GameResults.Winner = remainingRobots.First().TeamName;
                foreach (var remainingRobot in remainingRobots)
                {
                    gameState.GameResults.WinnersTotalForce += remainingRobot.Level;
                }
            }
            else
            {
                gameState.GameResults.Winner = "No winner";
                gameState.GameResults.WinnersTotalForce = 0;
            }

            gameState.GameResults.RoundsTaken = gameState.Turn + 1;
            gameState.Finished = true;
        }

        private void ResolveFieldConflicts(GameState gameState)
        {
            JoinRobots(gameState);
            FightRobots(gameState);
        }

        internal void FightRobots(GameState gameState)
        {
            var fightingPlaces = gameState.Robots.GroupBy(r => r.Position).Where(g => g.Count() > 1).ToList();
            foreach (var robotGroup in fightingPlaces)
            {
                FightRobotsOnSameField(gameState, robotGroup);
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

        internal void DoPowerUps(GameState gameState)
        {
            var removeCandidates = new List<PowerUp>();

            foreach (var powerUp in gameState.PowerUps)
            {
                var robotOnPowerup = gameState.Robots.FirstOrDefault(r => r.Position == powerUp.Position);
                if (robotOnPowerup == null)
                {
                    continue;
                }

                robotOnPowerup.Level += powerUp.Level;
                removeCandidates.Add(powerUp);
            }

            gameState.PowerUps = gameState.PowerUps.Except(removeCandidates).ToList();
        }


        internal void DoRobotMovements(GameState gameState)
        {
            var robotsToMove = gameState.Robots.Where(r => r.WaitTurns == 0 && r.CurrentAction != RobotActions.Idle).ToList();

            robotsToMove.ForEach(robot => robot.Position = GetPositionFromMovement(robot.Position, robot.CurrentDirection, gameState.Configuration.MapSize));
        }

        internal void GetNextTurnsFromRobots(GameState gameState)
        {
            var map = CreateRobotMap(gameState);

            var readyRobots = gameState.Robots.Where(robot => robot.WaitTurns == 0).ToList();

            foreach (var robot in readyRobots)
            {
                DoNextTurnForRobot(gameState, robot, map);
            }
        }

        private static Dictionary<Point, List<Robot>> CreateRobotMap(GameState gameState)
        {
            var result = new Dictionary<Point, List<Robot>>();

            gameState.Robots.ForEach(robot =>
            {
                if (!result.ContainsKey(robot.Position))
                {
                    result[robot.Position] = new List<Robot>();
                }
                result[robot.Position].Add(robot);
            });

            return result;
        }

        private static void DoNextTurnForRobot(GameState gameState, Robot robot, Dictionary<Point, List<Robot>> map)
        {
            var surroundings = new Surroundings();
            surroundings.Robots = GetSurroundingRobots(gameState, robot, map);
            surroundings.PowerUps = GetSurroundingPowerUps(gameState, robot);

            var nextTurn = new NextRobotTurn();
            try
            {
                nextTurn = robot.RobotImplementation.DoNextTurn(gameState.Turn, robot.Level, surroundings);
            }
            catch (Exception)
            {
                nextTurn.NextAction = RobotActions.Idle;
            }


            robot.CurrentAction = nextTurn.NextAction;
            robot.CurrentDirection = nextTurn.NextDirection;

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
            var currentRobotPosition = robot.Position;
            var robotDirection = robot.CurrentDirection;
            var mapSize = gameState.Configuration.MapSize;

            var newLevel = (int)Math.Ceiling(robot.Level / 2.0);

            var newPosition = GetPositionFromMovement(currentRobotPosition, robotDirection, mapSize);
            var newRobot = new Robot(newPosition, newLevel, robot.TeamName, robot.RobotImplementation);
            gameState.Robots.Add(newRobot);

            robot.Level = newLevel;
        }

        private void GeneratePowerUps(GameState gameState)
        {
            var propability = gameState.Configuration.PowerupPropability;
            var mapSize = gameState.Configuration.MapSize;
            var random = new Random();

            for (var x = 0; x < mapSize.Width; x++)
            {
                for (var y = 0; y < mapSize.Height; y++)
                {
                    var randomValue = random.Next(1, 100);
                    if (randomValue >= (100 - propability * 100) && gameState.PowerUps.All(p => p.Position != new Point(x, y)))
                    {
                        var level = Math.Max(gameState.Turn / 2, 1);
                        gameState.PowerUps.Add(new PowerUp() { Level = level, Position = new Point(x, y) });
                    }
                }
            }
        }

        internal static IEnumerable<SurroundingRobot> GetSurroundingRobots(GameState gameState, Robot robot, Dictionary<Point, List<Robot>> map)
        {
            var surroundingPositions = GetSurroundingPositions(robot.Position);

            var surroundingrobots = surroundingPositions.SelectMany(pos => map.ContainsKey(pos) ? map[pos] : new List<Robot>());
            
            return surroundingrobots.Select(r => new SurroundingRobot() { Level = r.Level, IsEnemy = r.TeamName != robot.TeamName, Direction = GetDirectionFromRelativePositions(robot.Position, r.Position) }).ToList();
        }

        internal static IEnumerable<SurroundingPowerUp> GetSurroundingPowerUps(GameState gameState, Robot robot)
        {
            var surroundingPositions = GetSurroundingPositions(robot.Position);

            var powerUps = gameState.PowerUps.Where(r => surroundingPositions.Contains(r.Position));

            return powerUps.Select(p => new SurroundingPowerUp() { Level = p.Level, Direction = GetDirectionFromRelativePositions(robot.Position, p.Position) }).ToList();
        }

        internal static Directions GetDirectionFromRelativePositions(Point position1, Point position2)
        {
            var relativePosition = position2.Substract(position1);

            if (relativePosition.Equals(new Point(-1, -1))) { return Directions.NW; }
            if (relativePosition.Equals(new Point(0, -1))) { return Directions.N; }
            if (relativePosition.Equals(new Point(1, -1))) { return Directions.NE; }
            if (relativePosition.Equals(new Point(-1, 0))) { return Directions.W; }
            if (relativePosition.Equals(new Point(1, 0))) { return Directions.E; }
            if (relativePosition.Equals(new Point(-1, 1))) { return Directions.SW; }
            if (relativePosition.Equals(new Point(0, 1))) { return Directions.S; }
            if (relativePosition.Equals(new Point(1, 1))) { return Directions.SE; }

            throw new Exception(string.Format("Invalid direction with robot {0} and surrounding robot {1}", position1, position2));
        }

        private static HashSet<Point> GetSurroundingPositions(Point position)
        {
            var result = new HashSet<Point>();

            result.Add(new Point(position.X - 1, position.Y - 1));
            result.Add(new Point(position.X, position.Y - 1));
            result.Add(new Point(position.X + 1, position.Y - 1));
            result.Add(new Point(position.X - 1, position.Y));
            result.Add(new Point(position.X + 1, position.Y));
            result.Add(new Point(position.X - 1, position.Y + 1));
            result.Add(new Point(position.X, position.Y + 1));
            result.Add(new Point(position.X + 1, position.Y + 1));

            return result;
        }

        internal static Point GetPositionFromMovement(Point robotPosition, Directions direction, Size mapSize)
        {
            var coordinatesChange = GetMovement(direction);

            var newPosition = new Point(robotPosition.X + coordinatesChange.X, robotPosition.Y + coordinatesChange.Y);

            if (newPosition.X < 0)
            {
                newPosition.X = mapSize.Width + newPosition.X;
            }

            if (newPosition.X >= mapSize.Width)
            {
                newPosition.X = newPosition.X - mapSize.Width;
            }

            if (newPosition.Y < 0)
            {
                newPosition.Y = mapSize.Height + newPosition.Y;
            }

            if (newPosition.Y >= mapSize.Height)
            {
                newPosition.Y = newPosition.Y - mapSize.Height;
            }

            return newPosition;
        }

        internal static Point GetMovement(Directions direction)
        {
            var x = 0;
            var y = 0;

            if (direction == Directions.E || direction == Directions.NE || direction == Directions.SE)
            {
                x++;
            }

            if (direction == Directions.W || direction == Directions.NW || direction == Directions.SW)
            {
                x--;
            }

            if (direction == Directions.N || direction == Directions.NE || direction == Directions.NW)
            {
                y--;
            }

            if (direction == Directions.S || direction == Directions.SW || direction == Directions.SE)
            {
                y++;
            }

            return new Point(x, y);
        }

        internal static List<string> GetTeamsAlive(GameState gameState)
        {
            return gameState.Robots.Select(r => r.TeamName).Distinct().ToList();
        }
    }


}
