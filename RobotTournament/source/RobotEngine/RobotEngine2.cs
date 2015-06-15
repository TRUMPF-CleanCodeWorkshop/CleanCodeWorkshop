using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace RobotEngine
{
    public class RobotEngine2 : IRobotEngine   
    {
        public string TeamName { get; private set; }
        public IRobot GetNewRobot()
        {
            return new Robot2();
        }

        public Color GetTeamColor()
        {
            return Color.Blue;
        }

        public RobotEngine2()
        {
            TeamName = "Robotnik";
        }
    }
}
