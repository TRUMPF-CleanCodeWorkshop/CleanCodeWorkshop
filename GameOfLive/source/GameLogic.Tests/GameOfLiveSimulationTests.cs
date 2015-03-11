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
        public void DetectReborn_Population_with_no_reborns_returns_null()
        {
            var population = new Cells() { 
                new Point(1, 1),
                new Point(1, 4),
                new Point(0, 2)
            };

            var reborns = GameOfLiveSimulation.DetectReborn(population);

            Assert.That(reborns.Count(), Is.EqualTo(0));
        }

        [Test]
        public void DetectReborn_Population_with_one_reborns_returns_celllist_with_one_cell()
        {
            var population = new Cells() { 
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 1)
            };

            var reborns = GameOfLiveSimulation.DetectReborn(population);

            Assert.That(reborns.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DetectReborn_Population_with_two_reborns_returns_celllist_with_two_cells()
        {
            var population = new Cells() { 
                new Point(1, 0),
                new Point(1, 1),
                new Point(1, 2),
                new Point(1, 4),
                new Point(2, 0),
                new Point(2, 1)
            };

            var reborns = GameOfLiveSimulation.DetectReborn(population);

            Assert.That(reborns.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DetectReborn_Population_with_one_reborn_returns_celllist_with_cell_at_1_0()
        {
            var population = new Cells() { 
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 1)
            };

            var reborns = GameOfLiveSimulation.DetectReborn(population);

            Assert.That(reborns.First().X, Is.EqualTo(1));
            Assert.That(reborns.First().Y, Is.EqualTo(0));
        }

    }
}
