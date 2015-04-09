namespace GameEngineTests
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using Contracts.Model;

    using GameEngine;

    using NUnit.Framework;

    public class MyGameEngineJoinTests
    {
        [Test]
        public void MultipleAlliedRobotsOnTheSameFieldAreCorrectlyJoined()
        {
            var state = new GameState()
            {
                Configuration = null,
                Finished = false,
                PowerUps = new List<PowerUp>(),
                Robots = new List<Robot>()
                {
                    new Robot(new Point(1, 1), 5, "Red", null),
                    new Robot(new Point(1, 1), 10, "Red", null)
                }
            };

            var sut = new MyGameEngine();
            sut.JoinRobots(state);

            Assert.AreEqual(1, state.Robots.Count);
            
            var survivor = state.Robots.Single();
            Assert.AreEqual(15, survivor.Level);
            Assert.AreEqual(new Point(1, 1), survivor.Position);
            Assert.AreEqual("Red", survivor.TeamName);
        }

        [Test]
        public void HostileRobotsOnTheSameFieldAreNotJoined()
        {
            var redOne = new Robot(new Point(1, 1), 5, "Red", null);
            var blueOne = new Robot(new Point(1, 1), 10, "Blue", null);

            var state = new GameState()
            {
                Configuration = null,
                Finished = false,
                PowerUps = new List<PowerUp>(),
                Robots = new List<Robot>()
                {
                    redOne,
                    blueOne
                }
            };

            var sut = new MyGameEngine();
            sut.JoinRobots(state);

            Assert.AreEqual(2, state.Robots.Count);

            var red = state.Robots.Single(r => r.TeamName == "Red");
            var blue = state.Robots.Single(r => r.TeamName == "Blue");
            Assert.AreEqual(redOne, red);
            Assert.AreEqual(blueOne, blue);
        }

        [Test]
        public void JoinDoesNotAffectFieldIfNoRobotsAreOnSameField()
        {
            var redOne = new Robot(new Point(1, 1), 5, "Red", null);
            var redTwo = new Robot(new Point(2, 2), 10, "Red", null);

            var state = new GameState()
            {
                Configuration = null,
                Finished = false,
                PowerUps = new List<PowerUp>(),
                Robots = new List<Robot>()
                {
                    redOne,
                    redTwo
                }
            };

            var sut = new MyGameEngine();
            sut.JoinRobots(state);

            Assert.AreEqual(2, state.Robots.Count);

            var redOnOne = state.Robots.Single(r => r.Position.X == 1);
            var redOnTwo = state.Robots.Single(r => r.Position.X == 2);
            Assert.AreEqual(redOne, redOnOne);
            Assert.AreEqual(redTwo, redOnTwo);
        }
    }
}
