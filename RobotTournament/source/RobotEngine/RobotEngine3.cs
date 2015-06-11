using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace RobotEngine
{
    public class RobotEngine3: IRobotEngine
    {
         public string TeamName { get; private set; }
        public IRobot GetNewRobot()
        {
            return new Robot3();
        }

        public RobotEngine3()
        {
            TeamName = "Team 3";
        }
    }
}
