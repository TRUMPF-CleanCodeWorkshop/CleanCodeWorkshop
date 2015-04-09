using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Contracts;
using Contracts.Model;
using Core;
using NSubstitute;
using NUnit.Framework;

namespace CoreTests
{
    public class FrameworkTests
    {
        [Test]
        public void CreateInitializeGameStateCreatesAValidGameState()
        {
            var gameEngine = Substitute.For<IGameEngine>();
            gameEngine
                .GetInitialRobotPositions(Arg.Any<Size>(), Arg.Any<int>())
                .Returns(info => new List<Point>() { new Point(1, 1) });

            var robot = Substitute.For<IRobotEngine>();
            robot.TeamName.Returns(info => "Team Rocket");

            var config = CreateTestConfiguration();

            var state = Framework.CreateInitializeGameState(
                config,
                gameEngine,
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
