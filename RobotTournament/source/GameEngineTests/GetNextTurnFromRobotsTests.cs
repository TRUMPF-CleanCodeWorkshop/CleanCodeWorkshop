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
        public void GetNextTurnsFromRobots_prüft_das_die_Ergebnisse_Der_Robot_Dlls_in_den_GameState_übernommen_werden()
        {
            var robotToCheck = Substitute.For<IRobot>();
            var defaultRobot = Substitute.For<IRobot>();

            defaultRobot.DoNextTurn(1, Arg.Any<int>(), Arg.Any<Surroundings>()).Returns(new NextRobotTurn() {NextAction = RobotActions.Moving, NextDirection = Directions.N});
            robotToCheck.DoNextTurn(1, Arg.Any<int>(), Arg.Any<Surroundings>()).Returns(new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = Directions.N });

            var gameEngine = new MyGameEngine();
            var gameState = new GameState()
            {
                Robots = new List<Robot>()
                {
                    new Robot(new Point(1, 1), 1 ,"feindlich", defaultRobot),
                    new Robot(new Point(2, 1), 1 ,"my", robotToCheck),
                    new Robot(new Point(3, 1), 1 ,"freund", defaultRobot)
                },
                Turn =1,
                PowerUps = new List<PowerUp>()
            };

            gameEngine.GetNextTurnsFromRobots(gameState);

            var targetRobot = gameState.Robots.Single(robot => robot.TeamName == "my");

            Assert.That(targetRobot.CurrentDirection, Is.EqualTo(Directions.N));
            Assert.That(targetRobot.CurrentAction, Is.EqualTo(RobotActions.Moving));

        } 

    }
}
