using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Model;

namespace DebugUI
{
    using System.Drawing;

    public static class GameUI
    {
        private const string EmptyRow = "           |";
        private const string UnderlineRow = "___________|";

        public static void ShowGameState(GameState gameState)
        {
            var numberOfColumns = gameState.Configuration.MapSize.Width;
            var numberOfRows = gameState.Configuration.MapSize.Height;
            Console.WindowWidth = numberOfColumns * 12 + 5;
            Console.WindowHeight = numberOfRows * 4 + 5;

            var firstRow = new StringBuilder("  |");
            var secondRow = new StringBuilder("__|");
            
            for (var columnCounter = 0; columnCounter < numberOfColumns; columnCounter++)
            {
                firstRow.Append(string.Format("{0}|", columnCounter.ToString().PadRight(11)));
                secondRow.Append(UnderlineRow);
            }
            Console.WriteLine(firstRow);
            Console.WriteLine(secondRow);

            for (var rowCounter = 0; rowCounter < numberOfRows; rowCounter++)
            {
                PrintRowWithRobots(rowCounter, numberOfColumns, gameState.Robots, gameState.PowerUps);
            }
        }

        public static void PrintRowWithRobots(int rowNumber, int mapWidth, IEnumerable<Robot> robots, IEnumerable<PowerUp> powerUps)
        {
            var firstRow = new StringBuilder("  |");

            var secondRow = new StringBuilder(string.Format("{0} |", rowNumber));

            var thirdRow = new StringBuilder("  |");

            var fourthRow = new StringBuilder("  |");
            for (var columnCounter = 0; columnCounter < mapWidth; columnCounter++)
            {
                var powerUp = powerUps.FirstOrDefault(p => p.Position == new Point(columnCounter, rowNumber));
                var robot = robots.FirstOrDefault(r => r.Position == new Point(columnCounter, rowNumber));
                if (robot != null)
                {
                    firstRow.Append(GetRowForLevelAndId(robot));
                    secondRow.Append(GetRowForName(robot));
                    thirdRow.Append(GetRowForAction(robot));
                } else if (powerUp != null)
                {
                    firstRow.Append(string.Format("{0}|", "PowerUp".PadRight(11)));
                    secondRow.Append(string.Format("{0}|", powerUp.Level.ToString().PadRight(11)));
                }
                else
                {
                    firstRow.Append(EmptyRow);
                    secondRow.Append(EmptyRow);
                    thirdRow.Append(EmptyRow);
                }

                fourthRow.Append(UnderlineRow);
            }

            Console.WriteLine(firstRow);
            Console.WriteLine(secondRow);
            Console.WriteLine(thirdRow);
            Console.WriteLine(fourthRow);
        }

        public static string GetRowForLevelAndId(Robot robot)
        {
            var levelString = robot.Level.ToString().PadRight(5);
            var idString = robot.Id.PadLeft(5);
            return string.Format("{0} {1}|", levelString, idString);
        }

        public static string GetRowForName(Robot robot)
        {
            var nameString = robot.TeamName.PadRight(11);

            return string.Format("{0}|", nameString.Substring(0, 11));
        }

        public static string GetRowForAction(Robot robot)
        {
            var actionString = robot.CurrentAction.ToString().PadRight(11);
            return string.Format("{0}|", actionString.Substring(0, 11));
        }
    }
}
