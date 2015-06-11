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
            var direction = Directions.NE;

            var resultTuple = MyGameEngine.GetMovement(direction);

            Assert.Equals(resultTuple, new Tuple<int, int>(-1, -1));
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
