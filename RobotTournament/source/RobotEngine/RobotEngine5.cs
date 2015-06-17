using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotEngine
{
    using System.Drawing;

    using Contracts;

    class RobotEngine5 : IRobotEngine
    {
        public string TeamName { get; private set; }

        public RobotEngine5()
        {
            this.TeamName = "DonRobot";
        }

        public IRobot GetNewRobot()
        {
            return new Robot5();
        }

        public Color GetTeamColor()
        {
            return Color.DarkGray;
        }
    }
}
