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
    }
}
