using System;
using System.Drawing;
using GameOfLife.Contracts;

namespace GameOfLife.BitmapAdapter
{
    using System.IO;
    using System.Runtime.InteropServices;

    public class CellLoader
    {
        public static Cells Load(string path, Size mapSize)
        {
            var bitmap = LoadBitmap(path);

            return CalculateInitalCells(bitmap, mapSize);
        }

        internal static Bitmap LoadBitmap(string path)
        {
            // var fileContent = File.ReadAllBytes(path);
            return new Bitmap(Bitmap.FromFile(path));
        }

        internal static Cells CalculateInitalCells(Bitmap bitmap, Size mapSize)
        {
            var cells = new Cells() { Generation = 0, Population = 0 };
            var bitmapSize = bitmap.Size;
            var startingPosition = new Point(mapSize.Width / 2 - bitmapSize.Width / 2, mapSize.Height / 2 - bitmapSize.Height / 2);

            for (var widthCount = 0; widthCount < bitmapSize.Width; widthCount++)
            {
                for (var heightCount = 0; heightCount < bitmapSize.Height; heightCount++)
                {
                    var pixel = bitmap.GetPixel(widthCount, heightCount);
                    if (pixel.R == 0 && pixel.G == 0 && pixel.B == 0)
                    {
                        cells.Add(new Point(startingPosition.X + widthCount, startingPosition.Y + heightCount));
                    }
                }
            }

            return cells;
        }
    }
}
