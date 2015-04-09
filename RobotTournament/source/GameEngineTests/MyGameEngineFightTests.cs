namespace GameEngineTests
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using Contracts.Model;

    using GameEngine;

    using NUnit.Framework;

    public class MyGameEngineFightTests
    {
        [Test]
        public void BothDead()
        {
            var redOne = new Robot(new Point(1, 1), 10, "Red", null);
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
            sut.FightRobots(state);

            Assert.AreEqual(0, state.Robots.Count);
        }

        [Test]
        public void HostileRobotsOnTheSameFieldFightEachOther()
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
            sut.FightRobots(state);

            Assert.AreEqual(1, state.Robots.Count);

            var blue = state.Robots.Single(r => r.TeamName == "Blue");
            Assert.AreEqual(blueOne, blue);
        }

        [Test]
        public void FightDoesNotAffectFieldIfNoRobotsAreOnSameField()
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
            sut.FightRobots(state);

            Assert.AreEqual(2, state.Robots.Count);

            var redOnOne = state.Robots.Single(r => r.Position.X == 1);
            var redOnTwo = state.Robots.Single(r => r.Position.X == 2);
            Assert.AreEqual(redOne, redOnOne);
            Assert.AreEqual(redTwo, redOnTwo);
        }
    }
}
