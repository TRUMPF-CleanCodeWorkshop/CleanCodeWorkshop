namespace GameEngineTests
{
    using System.Collections.Generic;
    using System.Drawing;
    using Contracts;
    using Contracts.Model;
    using GameEngine;
    using NSubstitute;
    using NUnit.Framework;

    internal class DoRobotMovementTests
    {
        [Test]
        public void RobotsAreMoved()
        {
            var configuration = new GameConfiguration()
            {
                MapSize = new Size(10, 10)
            };
            var engineToCheck = Substitute.For<IRobot>();
            var gameEngine = new MyGameEngine();
            var gameState = new GameState()
            {
                Robots = new List<Robot>()
                {
                    new Robot(new Point(0, 0), 1, "feindlich", Substitute.For<IRobot>())
                    {
                        WaitTurns = 0
                    },
                    new Robot(new Point(2, 1), 1, "freund", engineToCheck)
                    {
                        WaitTurns = 0
                    },
                    new Robot(new Point(3, 1), 1, "freund", Substitute.For<IRobot>())
                    {
                        WaitTurns = 1,
                        CurrentAction = RobotActions.Upgrading
                    }
                }, 
                Configuration = configuration
            };

            gameState.Robots[0].CurrentDirection = Directions.NE;
            gameState.Robots[1].CurrentDirection = Directions.N;
            gameState.Robots[2].CurrentDirection = Directions.S;

            gameEngine.DoRobotMovements(gameState);
            Assert.That(
                gameState.Robots[0].Position, 
                Is.EqualTo(new Point(1, gameState.Configuration.MapSize.Height - 1)));
            Assert.That(gameState.Robots[1].Position, Is.EqualTo(new Point(2, 0)));
            Assert.That(gameState.Robots[2].Position, Is.EqualTo(new Point(3, 1)));
        }



    }
}
