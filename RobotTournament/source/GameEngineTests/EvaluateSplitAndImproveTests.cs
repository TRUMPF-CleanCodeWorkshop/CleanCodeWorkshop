using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTests
{
    using System.Drawing;

    using Contracts;
    using Contracts.Model;

    using GameEngine;

    using NSubstitute;

    using NUnit.Framework;

    class EvaluateSplitAndImproveTests
    {
        [Test]
        public void RobotsAreImproved()
        {
            var engineToCheck = Substitute.For<IRobotEngine>();
            var gameEngine = new MyGameEngine();
            var gameState = new GameState()
            {
                Robots = new List<Robot>()
                {
                    new Robot(new Point(1, 1), 1 ,"feindlich", Substitute.For<IRobotEngine>()),
                    new Robot(new Point(2, 1), 1 ,"freund", engineToCheck) { WaitTurns = 1, CurrentAction = RobotActions.Upgrading},
                    new Robot(new Point(3, 1), 1 ,"freund", Substitute.For<IRobotEngine>()) {WaitTurns = 1, CurrentAction = RobotActions.Upgrading}
                }
            };

            gameEngine.EvaluateSplitAndImrove(gameState);

            Assert.That(gameState.Robots.Count(r => r.Level == 3), Is.EqualTo(2));
        }
    }
}
