using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugUITests
{
    using System.Drawing;

    using Contracts.Model;

    using DebugUI;

    using NUnit.Framework;

    public class GameUITests
    {
        [Test]
        public void RowForIdAndLevelIsGivenForARobot()
        {
            var robot = new Robot(new Point(0, 0), 42, "SuperTeam");

            var expectedOutput = string.Format("|42        {0}|", robot.Id);
            var actualOutput = GameUI.GetRowForLevelAndId(robot);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void RowForRobotTeamNameisValid()
        {
            var robot = new Robot(new Point(0, 0), 42, "SuperTeam");

            var expectedOutput = "|SuperTeam  |";
            var actualOutput = GameUI.GetRowForName(robot);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void RobotTeamNameWithMoreThanElevenCharactersIsHandledCorrectly()
        {
            var robot = new Robot(new Point(0, 0), 42, "DasSuperTeamMitVielenZeichenUndSo");

            var expectedOutput = "|DasSuperTea|";
            var actualOutput = GameUI.GetRowForName(robot);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }
    }
}
