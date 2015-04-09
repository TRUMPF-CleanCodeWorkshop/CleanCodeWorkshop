﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace RobotEngine
{
    public class RobotEngine1 : IRobotEngine   
    {
        public string TeamName { get; private set; }
        public IRobot GetNewRobot()
        {
            return new Robot1();
        }

        public RobotEngine1()
        {
            TeamName = "Team 1";
        }
    }
}
