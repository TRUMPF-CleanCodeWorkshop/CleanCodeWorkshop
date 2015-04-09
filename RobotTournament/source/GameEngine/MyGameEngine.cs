using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Model;

namespace GameEngine
{
    using System.Collections;
    using System.Drawing;

    public class MyGameEngine : IGameEngine
    {
        private static readonly Random Randomizer = new Random();

        public GameState CreateNextTurn(GameState gameState)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Point> GetInitialRobotPositions(Size mapSize, int count)
        {
            var results = new List<Point>();

            for (var i = 0; i < count; i++)
            {
                Point nextPoint;
                do
                {
                    nextPoint = new Point(Randomizer.Next(mapSize.Width), Randomizer.Next(mapSize.Height));
                }
                while (results.Contains(nextPoint));
                results.Add(nextPoint);
            }

            return results;
        }
    }
}
