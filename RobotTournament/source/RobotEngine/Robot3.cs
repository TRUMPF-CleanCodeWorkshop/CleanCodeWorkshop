using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Model;
namespace RobotEngine
{
    class Robot3: IRobot
    {
        public NextRobotTurn DoNextTurn(int currentTurn, int myLevel, Surroundings environment)
        {
            var random = new Random();
            var randomNumber = random.Next(1, 3);


            var decisionDirection = currentTurn % 23 == 0 ? Directions.NW : Directions.SW;
            
            if (environment.PowerUps.Any())
            {
                var firstPowerUpDirection = environment.PowerUps.First().Direction;
                return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = firstPowerUpDirection };
            }

            if (randomNumber == 1)
            {
                return new NextRobotTurn() {NextAction = RobotActions.Upgrading, NextDirection = decisionDirection};
            }

            if (environment.Robots.Any(r => r.IsEnemy && r.Level < myLevel))
            {
                return new NextRobotTurn()
                {
                    NextAction = RobotActions.Moving, 
                    NextDirection = environment.Robots.First(r => r.IsEnemy && r.Level < myLevel).Direction
                };
            }

            if (environment.Robots.Any(r => r.IsEnemy && r.Level > myLevel))
            {
                var directionOfFirstDangerousBot =
                    environment.Robots.First(r => r.IsEnemy && r.Level > myLevel).Direction;
                var directionToMove = directionOfFirstDangerousBot + 4;
                if ((int)directionToMove > 7)
                {
                    directionToMove -= 7;
                }
                return new NextRobotTurn() {NextAction = RobotActions.Moving, NextDirection = directionToMove};
            }

            return new NextRobotTurn() { NextAction = RobotActions.Moving, NextDirection = Directions.W };
        }


        }
    }
