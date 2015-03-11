namespace UI.Tests
{
    using System.Drawing;

    using GameOfLife.Contracts;

    using NUnit.Framework;

    public class MapDrawerTests
    {
        private Cells testCells = new Cells()
        {
            new Point(1, 1),
            new Point(20, 20),
            new Point(300, 300),
            new Point(5, 5),
            new Point(7, 7),
            new Point(-100, 5),
            new Point(1000, 34)
        };

        [Test]
        public void MapDrawerDraws5CellsIf5CellsAreInRange()
        {
        }

        [Test]
        public void MapDrawerIgnoresCellsOutOfRange()
        {
          

        }




    }
}
