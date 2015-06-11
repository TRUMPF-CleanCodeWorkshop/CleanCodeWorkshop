namespace GameEngineTests
{
    using System.Collections.Generic;
    using System.Drawing;
    using Contracts;
    using Contracts.Model;
    using GameEngine;
    using NSubstitute;
    using NUnit.Framework;

    internal class IncreaseTurnOrEndGameTests
    {
        [Test]
        public void TwoTeamsAlive()
        {
            var configuration = new GameConfiguration()
            {
                MapSize = new Size(10, 10)
            };
            var gameEngine = new MyGameEngine();
            var gameState = new GameState()
            {
                Robots = new List<Robot>()
                {
                    new Robot(new Point(0, 0), 1, "Team A", Substitute.For<IRobot>())
                    {
                        WaitTurns = 0
                    },
                    new Robot(new Point(2, 1), 1, "Team B", Substitute.For<IRobot>())
                    {
                        WaitTurns = 0
                    },
                }, 
                Configuration = configuration
            };

            var turnBefore = gameState.Turn;
            gameEngine.IncreaseTurnOrEndGame(gameState);
            Assert.That(gameState.Finished, Is.EqualTo(false));
            Assert.That(gameState.Turn, Is.EqualTo(turnBefore+1));
        }

        [Test]
        public void OnlyOneTeamAlive()
        {
            var configuration = new GameConfiguration()
            {
                MapSize = new Size(10, 10)
            };
            var gameEngine = new MyGameEngine();
            var gameState = new GameState()
            {
                Robots = new List<Robot>()
                {
                    new Robot(new Point(0, 0), 1, "Team A", Substitute.For<IRobot>())
                    {
                        WaitTurns = 0
                    },
                },
                Configuration = configuration
            };

            var turnBefore = gameState.Turn;
            gameEngine.IncreaseTurnOrEndGame(gameState);
            Assert.That(gameState.Finished, Is.EqualTo(true));
            Assert.That(gameState.Turn, Is.EqualTo(turnBefore));
        }

        [Test]
        public void AllTeamsDead()
        {
            var configuration = new GameConfiguration()
            {
                MapSize = new Size(10, 10)
            };
            var gameEngine = new MyGameEngine();
            var gameState = new GameState()
            {
                Robots = new List<Robot>(),
                Configuration = configuration
            };

            var turnBefore = gameState.Turn;
            gameEngine.IncreaseTurnOrEndGame(gameState); 
            Assert.That(gameState.Finished, Is.EqualTo(true));
            Assert.That(gameState.Turn, Is.EqualTo(turnBefore));
        }

    }
}
