using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTests
{
    using System.Drawing;

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

            // Assert
        }
    }
}
