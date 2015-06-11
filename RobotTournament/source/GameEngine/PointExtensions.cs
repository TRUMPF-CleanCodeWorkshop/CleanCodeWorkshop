using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public static class PointExtensions
    {
        public static Point Substract(this Point first, Point second)
        {
            return new Point(first.X - second.X, first.Y - second.Y);
        }
    }
}
