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
            var robot = new Robot(new Point(0, 0), 42, "SuperTeam", "123");

            var expectedOutput = "|42      123|";
            var actualOutput = GameUI.GetRowForLevelAndId(robot);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void RowForRobotTeamNameisValid()
        {
            var 
        }
    }
}
