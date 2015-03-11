using System;
using System.Drawing;
using GameOfLife.Contracts;

namespace GameOfLife.BitmapAdapter
{
    public class CellLoader
    {
        public static Cells Load(string path, Size mapSize)
        {
            var bitmap = LoadBitmap(path);

            return CalculateInitalCells(bitmap, mapSize);
        }

        internal static Bitmap LoadBitmap(string path)
        {
            throw new NotImplementedException();
        }

        internal static Cells CalculateInitalCells(Bitmap bitmap, Size mapSize)
        {
            throw new NotImplementedException();
        }
    }
}
