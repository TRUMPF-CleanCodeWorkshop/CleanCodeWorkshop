using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugUITests
{
    using Contracts.Model;

    using DebugUI;

    using NUnit.Framework;

    public class GameUITests
    {
        [Test]
        public void RowForIdAndLevelIsGivenForARobot()
        {
            var robot = new Robot() { Id = "123", Level = 42, TeamName = "SuperTeam" };

            var expectedOutput = "|42      123|";
            var actualOutput = GameUI.GetRowForLevelAndId(robot);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }
    }
}
