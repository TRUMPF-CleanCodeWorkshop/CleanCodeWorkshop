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
            var robotToCheck = Substitute.For<IRobot>();
            var defaultRobot = Substitute.For<IRobot>();

            defaultRobot.DoNextTurn(Arg.Any<int>(), Arg.Any<Surroundings>()).Returns(new NextRobotTurn() {NextAction = RobotActions.Moving, NextDirection = Directions.N});
            robotToCheck.DoNextTurn(Arg.Any<int>(), Arg.Any<Surroundings>()).Returns(new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = Directions.N });

            var gameEngine = new MyGameEngine();
            var gameState = new GameState()
            {
                Robots = new List<Robot>()
                {
                    new Robot(new Point(1, 1), 1 ,"feindlich", defaultRobot),
                    new Robot(new Point(2, 1), 1 ,"freund", robotToCheck),
                    new Robot(new Point(3, 1), 1 ,"freund", defaultRobot)
                }
            };

            gameEngine.GetNextTurnsFromRobots(gameState);



        } 

    }
}
