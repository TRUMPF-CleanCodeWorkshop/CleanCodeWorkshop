using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTests
{
    using System.Drawing;
    using System.Runtime;
    using System.Security.Cryptography;

    using Contracts.Model;

    using GameEngine;

    using NUnit.Framework;

    using RobotEngine;

    class SplitRobotTests
    {
        [Test]
        public void MovementToNorthWestGivesRightMovment()
        {
            var direction = Directions.NW;

            var resultTuple = MyGameEngine.GetMovement(direction);

            Assert.AreEqual(resultTuple, new Point(-1, -1));
        }

        [Test]
        public void MovementToEastGivesRightMovment()
        {
            var direction = Directions.E;

            var resultTuple = MyGameEngine.GetMovement(direction);

            Assert.AreEqual(resultTuple, new Point(1, 0));
        }

        [Test]
        public void MovementToSouthEastGivesRightMovment()
        {
            var direction = Directions.SE;

            var resultTuple = MyGameEngine.GetMovement(direction);

            Assert.AreEqual(resultTuple, new Point(1, 1));
        }

        [Test]
        public void PositionAfterSplitIsDeterminedCorrectlyWhenMovingToEast()
        {
            // Arrange
            var size = new Size(10, 10);
            var currentPosition = new Point(5, 5);
            var direction = Directions.E;

            // Act
            var newPosition = MyGameEngine.GetPositionFromMovement(currentPosition, direction, size);

            // Assert
            Assert.AreEqual(new Point(6, 5), newPosition);
        }

        [Test]
        public void NewPositionIsDeterminedCorrectlyIfCurrentPositionIsOnBorder()
        {
            var position = new Point(4, 4);
            var mapSize = new Size(5, 5);
            var direction = Directions.SE;

            var newPosition = MyGameEngine.GetPositionFromMovement(position, direction, mapSize);

            Assert.AreEqual(new Point(0, 0), newPosition);
        }

        [Test]
        public void RobotSplitIsPerformedCorrectly()
        {
            var originalRobot = new Robot(new Point(2, 2), 5, "Super Team", new Robot1());
            originalRobot.CurrentAction = RobotActions.Splitting;
            originalRobot.CurrentDirection = Directions.W;
            var gameState = new GameState()
                                {
                                    Configuration = new GameConfiguration() { MapSize = new Size(5, 5) },
                                    Finished = false,
                                    Robots = new List<Robot>() { originalRobot }
                                };

            var gameEngine = new MyGameEngine();
            gameEngine.PerformSplit(originalRobot, gameState);

            var newRobotList = gameState.Robots.Where(r => r.Position == new Point(1, 2)).ToList();

            Assert.AreEqual(originalRobot.Level, 3);
            Assert.AreEqual(gameState.Robots.Count, 2);
            Assert.AreEqual(1, newRobotList.Count);
            Assert.AreEqual(newRobotList.First().Level, 3);
        }
    }
}
