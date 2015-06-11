using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Model;
using GameEngine;
using System.Drawing;
using NSubstitute;

using NUnit.Framework;
namespace GameEngineTests
{
    class DoRobotMovementTests
    {
        [Test]
        public void robotsAreMoved() 
        {
            var engineToCheck = Substitute.For<IRobot>();
            var gameEngine = new MyGameEngine();
            var gameState = new GameState()
            {
                Robots = new List<Robot>()
                {
                    new Robot(new Point(1, 1), 1 ,"feindlich", Substitute.For<IRobot>()),
                    new Robot(new Point(2, 1), 1 ,"freund", engineToCheck) { WaitTurns = 1, CurrentAction = RobotActions.Upgrading},
                    new Robot(new Point(3, 1), 1 ,"freund", Substitute.For<IRobot>()) {WaitTurns = 1, CurrentAction = RobotActions.Upgrading}
                }
            };

            gameEngine.DoRobotMovements(gameState);
            Assert.That(gameState.Robots[0].Position, Is.EqualTo(new Point(1,2)));
            Assert.That(gameState.Robots[1].Position, Is.EqualTo(new Point(1, 2)));
            Assert.That(gameState.Robots[2].Position, Is.EqualTo(new Point(1, 2)));
        }



    }
}
