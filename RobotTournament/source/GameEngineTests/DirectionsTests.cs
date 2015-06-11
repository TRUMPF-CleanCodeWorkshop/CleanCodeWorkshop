using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Model;
using GameEngine;
using NUnit.Framework;

namespace GameEngineTests
{
    [TestFixture]
    public class DirectionsTests
    {
        [Test]
        public void GetDirectionFromRelativePositions_liefert_North_für_einen_Roboter_der_nördlich_steht()
        {
            var myRobotPosition = new Point(1, 1);
            var otherRobotPosition = new Point(1, 0);

            var direction = MyGameEngine.GetDirectionFromRelativePositions(myRobotPosition, otherRobotPosition);

            Assert.That(direction, Is.EqualTo(Directions.N));
        }

    }
}
