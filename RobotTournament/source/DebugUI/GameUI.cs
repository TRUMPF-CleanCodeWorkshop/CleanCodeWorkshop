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
        private const string EmptyRow = "|            ";

        public static void ShowGameState(GameState gameState)
        {
            var numberOfColumns = gameState.Configuration.MapSize.Width;
            Console.WindowWidth = numberOfColumns * 13 + 5;

            var firstRow = new StringBuilder("  |");
            
            for (var columnCounter = 0; columnCounter < numberOfColumns; columnCounter++)
            {
                firstRow.Append(columnCounter.ToString().PadRight(13));
            }
            Console.WriteLine(firstRow);

            for (var rowCounter = 0; rowCounter < gameState.Configuration.MapSize.Height; rowCounter++)
            {
                PrintRowWithRobots(rowCounter, numberOfColumns, gameState.Robots);
            }
        }

        public static void PrintRowWithRobots(int rowNumber, int mapWidth, IEnumerable<Robot> robots)
        {
            var firstRow = new StringBuilder("  |");

            var secondRow = new StringBuilder(string.Format("{0} |", rowNumber));

            var thirdRow = new StringBuilder("  |");
            for (var columnCounter = 0; columnCounter < mapWidth; columnCounter++)
            {
                var robot = robots.FirstOrDefault(r => r.Position == new Point(columnCounter, rowNumber));
                if (robot != null)
                {
                    firstRow.Append(GetRowForLevelAndId(robot));
                    secondRow.Append(GetRowForName(robot));
                }
                else
                {
                    firstRow.Append(EmptyRow);
                    secondRow.Append(EmptyRow);
                }
                thirdRow.Append(EmptyRow);
            }

            Console.WriteLine(firstRow);
            Console.WriteLine(secondRow);
            Console.WriteLine(thirdRow);
        }

        public static string GetRowForLevelAndId(Robot robot)
        {
            var levelString = robot.Level.ToString().PadRight(5);
            var idString = robot.Id.PadLeft(5);
            return string.Format("|{0} {1}|", levelString, idString);
        }

        public static string GetRowForName(Robot robot)
        {
            var nameString = robot.TeamName.PadRight(11);

            return nameString.Substring(0, 11);
        }

        public static string GetBorderRowForRobot()
        {
            return ""
        }
    }
}
