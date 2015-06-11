using System;
using System.Linq;
using Contracts;
using Contracts.Model;

namespace RobotEngine
{
    public class Robot1 : IRobot
    {
        private int upgrade = 85;
        private int split = 20;
        private int move = 0;

        private Random random = new Random();

        public NextRobotTurn DoNextTurn(int currentTurn, int myLevel, Surroundings environment)
        {
            var decisionAction = random.Next(100);
            var decisionDirection = (Directions)random.Next(0, 7);

            if (currentTurn == 5) VeryEarlyGameSettings();
            if (currentTurn == 15) EarlyGameSettings();
            if (currentTurn == 60) MidGameSettings();
            if (currentTurn == 100) MidLateGameSettings();
            if (currentTurn == 200) LateGameSettings();


            if (environment.PowerUps.Any())
            {
                return new NextRobotTurn() {NextAction = RobotActions.Moving, NextDirection = environment.PowerUps.First().Direction};
            }

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
            upgrade = 90;
            split = 20;
            move = 10;
        }

        private void EarlyGameSettings()
        {
            upgrade = 90;
            split = 40;
            move = 10;
        }

        private void MidGameSettings()
        {
            upgrade = 90;
            split = 50;
            move = 0;
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
