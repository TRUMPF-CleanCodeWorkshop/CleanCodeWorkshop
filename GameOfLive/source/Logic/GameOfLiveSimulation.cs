using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.Contracts;

namespace GameOfLife.GameLogic
{
    public class GameOfLiveSimulation
    {
        public static Cells CalculateNextGeneration(Cells previousGeneration)
        {
            var survivals = DetectSurvivals(previousGeneration);
            var reborn = DetectReborn(previousGeneration);

            return GenerateNextPopulation(survivals, reborn, previousGeneration);
        }

        internal static Cells GenerateNextPopulation(Cells survivals, Cells reborn, Cells previousGeneration)
        {
            var newGeneration = new Cells();
            newGeneration.AddRange(survivals);
            newGeneration.AddRange(reborn);
            newGeneration.Population = newGeneration.Count;
            newGeneration.Generation = previousGeneration.Generation + 1;
            return newGeneration;
            
        }

        internal static Cells DetectReborn(Cells previousGeneration)
        {
            throw new NotImplementedException();
        }

        internal static Cells DetectSurvivals(Cells previousGeneration)
        {
            var newGeneration = new Cells();
            newGeneration.AddRange(previousGeneration.Where(x => CalculateNeighbors(x, previousGeneration) == 2 || CalculateNeighbors(x, previousGeneration) == 3));
            
            return newGeneration;
        }

        internal static int CalculateNeighbors(Point cell, Cells generation)
        {
            var environment = GenerateEnvironment(cell);

            return environment.Count(generation.Contains);
        }

        internal static IEnumerable<Point> GenerateEnvironment(Point cell)
        {
            yield return new Point(cell.X - 1, cell.Y - 1);
            yield return new Point(cell.X - 1, cell.Y);
            yield return new Point(cell.X - 1, cell.Y + 1);
            yield return new Point(cell.X, cell.Y - 1);
            yield return new Point(cell.X, cell.Y + 1);
            yield return new Point(cell.X + 1, cell.Y - 1);
            yield return new Point(cell.X + 1, cell.Y);
            yield return new Point(cell.X + 1, cell.Y + 1);
        }
    }
}
