namespace GameEngineTests
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using Contracts.Model;

    using GameEngine;

    using NUnit.Framework;

    [TestFixture]
    public class MyGameEngineDoPowerUpsTests
    {
        [Test]
        public void TestOneRobotReceivingPowerUp()
        {
            var gameState = SetupGameState();
            var sut = new MyGameEngine();

            sut.DoPowerUps(gameState);

            Assert.That(11, Is.EqualTo(gameState.Robots.First().Level));
        }

        [Test]
        public void TestTwoOfThreeRobotReceivingPowerUp()
        {
            var gameState = SetupGameState();
            var sut = new MyGameEngine();

            sut.DoPowerUps(gameState);

            Assert.That(11, Is.EqualTo(gameState.Robots.First().Level));
            Assert.That(6, Is.EqualTo(gameState.Robots.Skip(1).Take(1).First().Level));
            Assert.That(1, Is.EqualTo(gameState.Robots.Skip(2).Take(1).First().Level));
        }

        private static GameState SetupGameState()
        {
            var robots = new List<Robot>
                             {
                                 new Robot(new Point(1, 1), 1, "TeamA", null),
                                 new Robot(new Point(1, 2), 1, "TeamB", null),
                                 new Robot(new Point(1, 3), 1, "TeamA", null)
                             };

            var powerUps = new List<PowerUp>
                               {
                                   new PowerUp() { Level = 10, Position = new Point(1, 1) },
                                   new PowerUp() { Level = 5, Position = new Point(1, 2) },
                                   new PowerUp() { Level = 1000, Position = new Point(1, 4) }
                               };

            var gameState = new GameState() { Robots = robots, PowerUps = powerUps };
            return gameState;
        }

        [Test]
        public void TestPowerUpWasDeteled()
        {
            var gameState = SetupGameState();
            var sut = new MyGameEngine();

            sut.DoPowerUps(gameState);

            Assert.That(1, Is.EqualTo(gameState.PowerUps.Count()));
        }

        [Test]
        public void TestRemovingPowerUpAfterDistribution()
        {
            
        }

        [Test]
        public void Tbd()
        {
            
        }
    }
}
