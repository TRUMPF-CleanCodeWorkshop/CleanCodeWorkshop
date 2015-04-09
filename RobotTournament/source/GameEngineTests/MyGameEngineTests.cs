namespace GameEngineTests
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using Contracts;
    using Contracts.Model;

    using GameEngine;

    using NSubstitute;

    using NUnit.Framework;

    public class MyGameEngineTests
    {
         [Test]
        public void CreateInitializeGameStateCreatesAValidGameState()
        {
            var gameEngine = new MyGameEngine();

            var robot = Substitute.For<IRobotEngine>();
            robot.TeamName.Returns(info => "Team Rocket");

            var config = CreateTestConfiguration();

            var state = gameEngine.CreateInitializeGameState(
                config,
                new List<IRobotEngine>() { robot });

            Assert.NotNull(state);
            Assert.AreEqual(1, state.Robots.Count);
            Assert.AreEqual(config, state.Configuration);
            Assert.AreEqual(0, state.Turn);

            var bot = state.Robots.Single();
            Assert.AreEqual("Team Rocket", bot.TeamName);
        }


        private static GameConfiguration CreateTestConfiguration()
        {
            return new GameConfiguration()
            {
                MapSize = new Size(5, 5),
                PowerupPropability = 1 / 2,
                RobotStartLevel = 1
            };
        }
    }
}
