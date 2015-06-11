using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Model;

namespace RobotEngine
{
    public class Robot1: IRobot
    {
        public NextRobotTurn DoNextTurn(int currentTurn, int myLevel, Surroundings environment)
        {
            var random = new Random();
            var decisionAction = random.Next(100);
            var decisionDirection = (Directions) random.Next(0, 7);

            if (decisionAction > 80)
            {
                return new NextRobotTurn() {NextAction = RobotActions.Upgrading, NextDirection = decisionDirection};
            }else if (decisionAction  >60)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Splitting , NextDirection = decisionDirection };
            }
            else if (decisionAction > 10)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Moving , NextDirection = decisionDirection };
            }
            else
            {
                return new NextRobotTurn() { NextAction = RobotActions.Idle, NextDirection = decisionDirection };
            }
        }
    }
}
