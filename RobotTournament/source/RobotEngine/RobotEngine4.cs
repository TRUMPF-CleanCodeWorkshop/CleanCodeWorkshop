using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace RobotEngine
{
    public class RobotEngine4 : IRobotEngine
    {
        public string TeamName { get; private set; }
        public IRobot GetNewRobot()
        {
            return new Robot4();
        }

        public Color GetTeamColor()
        {
            return Color.White;
        }
         
        public RobotEngine4()
        {
            TeamName = "Team 4";
        }
    }
}
