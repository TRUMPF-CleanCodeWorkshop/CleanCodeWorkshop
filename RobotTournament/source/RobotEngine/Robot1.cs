using System;
using Contracts;
using Contracts.Model;

namespace RobotEngine
{
    public class Robot1 : IRobot
    {
        private int upgrade = 10;
        private int split = 9;
        private int move = 8;

        public NextRobotTurn DoNextTurn(int currentTurn, int myLevel, Surroundings environment)
        {
            var random = new Random();
            var decisionAction = random.Next(100);
            var decisionDirection = (Directions)random.Next(0, 7);

            if (currentTurn == 10) VeryEarlyGameSettings();
            if (currentTurn == 30) EarlyGameSettings();
            if (currentTurn == 60) MidGameSettings();
            if (currentTurn == 100) MidLateGameSettings();
            if (currentTurn == 200) LateGameSettings();


            if (decisionAction > upgrade)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Upgrading, NextDirection = decisionDirection };
            }
            else if (decisionAction > split)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Splitting, NextDirection = decisionDirection };
            }
            else if (decisionAction > move)
            {
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = decisionDirection };
            }
            else
            {
                return new NextRobotTurn() { NextAction = RobotActions.Idle, NextDirection = decisionDirection };
            }
        }

        private void VeryEarlyGameSettings()
        {
            upgrade = 60;
            split = 20;
            move = 5;
        }

        private void EarlyGameSettings()
        {
            upgrade = 80;
            split = 40;
            move = 5;
        }

        private void MidGameSettings()
        {
            upgrade = 80;
            split = 60;
            move = 5;
        }

        private void MidLateGameSettings()
        {
            upgrade = 95;
            split = 75;
            move = 0;
        }

        private void LateGameSettings()
        {
            upgrade = 100;
            split = 90;
            move = 0;
        }
    }
}
