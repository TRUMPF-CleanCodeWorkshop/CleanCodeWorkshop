using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapAdapter.Tests
{
    using System.Drawing;
    using System.IO;

    using GameOfLife.BitmapAdapter;

    using NUnit.Framework;

    public class CellLoaderTests
    {
        [Test]
        public void LoadBitmap_Load_file_is_successfull()
        {
            var path = @"TestImages\glider.gif";
            var bitmap = CellLoader.LoadBitmap(path);

            Assert.That(bitmap, !Is.Null);
        }

        [Test]
        public void LoadBitmap_3_x_3_File_returns_3_x_3_Bitmap_Object()
        {
            var path = @"TestImages\glider.gif";
            var bitmap = CellLoader.LoadBitmap(path);

            Assert.That(bitmap.Size, Is.EqualTo(new Size(3, 3)));
        }

        [Test]
        public void CellLoader_glider_returns_correct_points()
        {
            var path = @"TestImages\glider.gif";
            var bitmap = CellLoader.LoadBitmap(path);

            var cellList = CellLoader.CalculateInitalCells(bitmap, new Size(3, 3));

            Assert.That(cellList.Contains(new Point(0, 1)));
            Assert.That(cellList.Contains(new Point(2, 2)));
            Assert.That(cellList.Contains(new Point(1, 1)), Is.EqualTo(false));
        }

        [Test]
        public void CellLoader_glider_is_placed_in_correct_position()
        {
            var path = @"TestImages\glider.gif";
            var bitmap = CellLoader.LoadBitmap(path);

            var cellList = CellLoader.CalculateInitalCells(bitmap, new Size(500, 500));

            Assert.That(cellList.Contains(new Point(249, 250)));
            Assert.That(cellList.Contains(new Point(251, 251)));
            Assert.That(cellList.Contains(new Point(249, 249)), Is.EqualTo(false));
        }
    }
}
