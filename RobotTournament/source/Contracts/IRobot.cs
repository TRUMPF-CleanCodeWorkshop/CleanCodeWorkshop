﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRobot
    {
        NextRobotTurn DoNextTurn(int currentTurn, int myLevel, Surroundings environment);
    }
}
