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
    }
}
