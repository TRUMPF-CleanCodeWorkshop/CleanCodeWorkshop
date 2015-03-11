using System.Collections.Generic;

namespace GameOfLife.UI
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Windows.Media.Imaging;

    using GameOfLife.Contracts;

    public class MapDrawer
    {
        public static BitmapSource DrawMap(int mapWidth, int mapHeight, Cells cells)
        {
            var bitmap = new Bitmap(mapWidth, mapHeight);

            using (var drawer = Graphics.FromImage(bitmap))
            {
                drawer.Clear(Color.White);

                var cellsInVisibleRange = cells.Where(cell => 
                    cell.X >= 0 && cell.Y >= 0 && 
                    cell.X < mapWidth && cell.Y < mapHeight).ToList();

                cellsInVisibleRange.ForEach(cell => bitmap.SetPixel(cell.X, cell.Y, Color.Black));
            }
            return MapDrawer.ConvertBitmapToBitmapSource(bitmap);
        }

        public static BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    // You need to specify the image format to fill the stream. 
                    // I'm assuming it is PNG
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    var bitmapDecoder = BitmapDecoder.Create(
                        memoryStream,
                        BitmapCreateOptions.PreservePixelFormat,
                        BitmapCacheOption.OnLoad);

                    // This will disconnect the stream from the image completely...
                    var writable = new WriteableBitmap(bitmapDecoder.Frames.Single());
                    writable.Freeze();

                    return writable;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
