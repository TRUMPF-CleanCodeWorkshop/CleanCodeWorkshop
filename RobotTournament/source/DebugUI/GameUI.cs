using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Model;

namespace DebugUI
{
    public static class GameUI
    {
        public static void ShowGameState(GameState gameState)
        {
            
        }

        public static string GetRowForLevelAndId(Robot robot)
        {
            var levelString = robot.Level.ToString().PadRight(5);
            var idString = robot.Id.PadLeft(5);
            return string.Format("|{0} {1}|", levelString, idString);
        }
    }
}
