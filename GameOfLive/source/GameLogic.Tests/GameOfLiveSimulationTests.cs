using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.Contracts;
using GameOfLife.GameLogic;
using NUnit.Framework;

namespace GameLogic.Tests
{
    public class GameOfLiveSimulationTests
    {
        [Test]
        public void CalculateNeighbors_Cell_empty_List_returns_0()
        {
            // Arange
            var cell = new Point(1, 1);
            var population = new Cells();

            //Act
            var count = GameOfLiveSimulation.CalculateNeighbors(cell, population);

            //Assert
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNeighbors_Cell_with_no_Neighbors_returns_0()
        {
            // Arange
            var cell = new Point(1, 1);
            var population = new Cells() { new Point(100, 100), new Point(3, 1), new Point(-1, 1) };

            //Act
            var count = GameOfLiveSimulation.CalculateNeighbors(cell, population);

            //Assert
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNeighbors_Cell_with_3_Neighbors_returns_3()
        {
            var cell = new Point(1, 1);
            var population = new Cells() { new Point(0, 1), new Point(2, 1), new Point(0, 0) };

            var count = GameOfLiveSimulation.CalculateNeighbors(cell, population);

            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void CalculateNeighbors_Cell_does_not_count_itself()
        {
            var cell = new Point(1, 1);
            var population = new Cells() { new Point(1, 1) };

            var count = GameOfLiveSimulation.CalculateNeighbors(cell, population);

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNeighbors_Cell_with_8_Neighbors_returns_8()
        {
            var cell = new Point(1, 1);
            var population = new Cells() { new Point(0, 1),
                new Point(2, 1),
                new Point(0, 0),
                new Point(0, 2),
                new Point(1, 0),
                new Point(1, 2),
                new Point(2, 0),
                new Point(2, 2) };

            var count = GameOfLiveSimulation.CalculateNeighbors(cell, population);

            Assert.That(count, Is.EqualTo(8));
        }


        [Test]
        public void DetectSurvivals_4_Cells_with_0_Survivors()
        {
            var population = new Cells() {
                new Point(2, 0),
                new Point(3, 0),
                new Point(0, 2),
                new Point(2, 2) };

            var cells = GameOfLiveSimulation.DetectSurvivals(population);

            Assert.That(cells.Count, Is.EqualTo(0));
        }

        [Test]
        public void DetectSurvivals_5_Cells_with_4_Survivors()
        {
            var population = new Cells() {
                new Point(2, 0),
                new Point(3, 0),
                new Point(1, 1),
                new Point(2, 1),
                new Point(0, 2),
                };

            var cells = GameOfLiveSimulation.DetectSurvivals(population);

            Assert.That(cells.Count, Is.EqualTo(4));
        }
       
        [Test]
        public void DetectSurvivals_Cells_with_4_Survivors_And_3_Dying_With_Overpopulation()
        {
            var population = new Cells() {
                new Point(2, 0),
                new Point(3, 0),
                new Point(0, 1),
                new Point(1, 1),
                new Point(2, 1),
                new Point(0, 2),
                new Point(1, 2),
                };

            var cells = GameOfLiveSimulation.DetectSurvivals(population);

            Assert.That(cells.Count, Is.EqualTo(4));
        }

        [Test]
        public void GenerateNextPopulation_Cells_with_2_Survivors_And_2_Reborns_TargetGeneration_2_Count_4()
        {
            var survivors = new Cells() { new Point(2, 0), new Point(3, 0) };
            var reborns = new Cells() { new Point(3, 1), new Point(2, 5) };
            var previousGeneration = new Cells() { Generation = 1 };
            var newGeneration = GameOfLiveSimulation.GenerateNextPopulation(survivors, reborns, previousGeneration);

            Assert.That(newGeneration.Count, Is.EqualTo(4));
            Assert.That(newGeneration.Generation, Is.EqualTo(2));
            Assert.That(newGeneration.Population, Is.EqualTo(4));

        }
    }
}
