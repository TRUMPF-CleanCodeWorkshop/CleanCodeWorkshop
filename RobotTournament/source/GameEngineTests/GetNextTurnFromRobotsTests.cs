using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Model;
using GameEngine;
using NSubstitute;
using NUnit.Framework;

namespace GameEngineTests
{
    public class GetNextTurnFromRobotsTests
    {
        [Test]
        public void GetNextTurnsFromRobots_()
        {
            var engineToCheck = Substitute.For<IRobotEngine>();
            var gameEngine = new MyGameEngine();
            var gameState = new GameState()
            {
                Robots = new List<Robot>()
                {
                    new Robot(new Point(1, 1), 1 ,"feindlich", Substitute.For<IRobotEngine>()),
                    new Robot(new Point(2, 1), 1 ,"freund", engineToCheck),
                    new Robot(new Point(3, 1), 1 ,"freund", Substitute.For<IRobotEngine>())
                }

            };
        } 

    }
}
